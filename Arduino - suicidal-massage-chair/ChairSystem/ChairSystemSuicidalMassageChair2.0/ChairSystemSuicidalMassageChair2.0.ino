#include <FastLED.h>
#include <Bounce2.h>
#include <ArduinoJson.h>

//PINMAPPINGS
#define pump A5
#define mssgup 8
#define mssgdown 7
#define kneading 10
#define pounding 9
#define feet 11

#define vibration 13
#define outside 5
#define legs 4
#define arms 3
#define shoulders 2

#define chairup A3
#define chairdown A4

#define topstop A1
#define botstop A2

#define redgreen A0

//still available 0, 1, 6, 12 (?)

//EDITABLE VARIABLES
unsigned int blinkTime = 2000;

int chair_position_estimated; //up: 0 - flat: 10000
int chair_position_target;  // 0 - 10000
int chair_position_motor_direction; //-1 down, 0 neutral, 1 up
int chair_position_move_time_max;
int chair_position_move_time_up;
int chair_position_move_time_down;

bool roller_kneading_on;
int roller_kneading_speed = 255;

bool roller_pounding_on;
int roller_pounding_speed = 255;

int roller_position_estimated;  //bottom: 0 - top: 10000
int roller_position_target;  // 0 - 10000
int roller_position_motor_direction; //-1 down, 0 neutral, 1 up
Bounce roller_sensor_top = Bounce();
Bounce roller_sensor_bottom = Bounce();
int roller_move_time_up = 15862; //is average measured value
int roller_move_time_down = 14776; //is average measured value
int roller_estimated_position;

bool feet_roller_on;
int feet_roller_speed = 255;

//bool airpump_on;
//bool airbag_shoulders_on;
//bool airbag_arms_on;
//bool airbag_legs_on;
//bool airbag_outside_on;
int airbag_time_max;

//bool butt_vibration_on;

bool backlight_on;
int backlight_color[] = {0, 0, 0}; //rgb
int backlight_LED[] = {0, 0}; //(led, color):
int blacklight_program[] = {0, 1, 2, 3}; //(program, parameters....)

bool redgreen_statuslight;//0=red,1=green

int button_bounce_time = 5;

unsigned long time_since_started;

int maxStringLength = 64;

String last_command = "";

//~~~~commands

//stop_all
//reset
//calibrate

//ack (sends status of everything)    ->    example=    chair_estimated_position:842;chair_position:neutral;chair_position_move_time_max:1949 ..... blacklight_program:rainbow,red,blue,purple;redgreen_statuslight:red;
//noack
//status (sends ack)

//UTILITY VARIABLES
unsigned long blinkTimer = 0;
int led = 13;
String readString;

const int outputs[] = {pump, mssgup, mssgdown, kneading, pounding, feet, vibration, outside, legs, arms, shoulders, led, chairup, chairdown, redgreen};
const int outputAmount = sizeof(outputs) / sizeof(outputs[0]);
const int inputs[] = {topstop, botstop};
const int inputAmount = sizeof(inputs) / sizeof(inputs[0]);

#define DATA_PIN    6
#define LED_TYPE    WS2811
#define COLOR_ORDER GRB
#define NUM_LEDS    39
CRGB leds[NUM_LEDS];
#define BRIGHTNESS          128
#define FRAMES_PER_SECOND  120

int ledPos = 0;
int ledBreathMin = 100;
int ledBreathMax = 255;

bool readingMessage = false;

#define MAXARRAYSIZE 4

String serial_error = "";
StaticJsonDocument<200> doc;

bool movingToTarget = true;//needed for roller

void setup() {
  Serial.begin(9600);
  while (!Serial);//leonardo fix?



  pinMode(led, OUTPUT);
  digitalWrite(led, HIGH);

  for (int i = 0; i < outputAmount; i++)
    pinMode(outputs[i], OUTPUT);
  //for (int i = 0; i < inputAmount; i++)
  //  pinMode(inputs[i], INPUT);

  FastLED.addLeds<LED_TYPE, DATA_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection(TypicalLEDStrip);
  FastLED.setBrightness(BRIGHTNESS);

  roller_sensor_top.attach(topstop, INPUT);
  roller_sensor_top.interval(button_bounce_time);
  roller_sensor_bottom.attach(botstop, INPUT);
  roller_sensor_bottom.interval(button_bounce_time);


  moveRollerUp();

  //rollerCalibrationRoutine();
  roller_position_target = 8000;
  //sendAck();
}



void loop()
{
  rollerRoutine();





  //Blinking the led to see if code is still running
  if (millis() > blinkTimer + blinkTime)
  {
    digitalWrite(led, !digitalRead(led));
    blinkTimer = millis();
  }

  if (Serial.available())  {
    char c = Serial.read();  //gets one byte from serial buffer
    if (c == '{' && !readingMessage)readingMessage = true;
    if (readingMessage) {
      if (c == '}') {  //looks for end of data packet marker
        readString += c;
        receiveMessage(readString); //prints string to serial port out
        //do stuff with captured readString
        readString = ""; //clears variable for new input
        readingMessage = false;
      }
      else {
        readString += c; //makes the string readString
        if (readString.length() > maxStringLength) {//preventing buffer overflow
          readString = "";
          readingMessage = false;
          printError("overflow");
        }
      }

    }
  }
}

void printError(String error) {
  Serial.print(F("{\n\t\"error\":\""));
  Serial.print(error);
  Serial.println(F("\"\n}"));
}


void incorrectMessage(String mssg) {
  Serial.print(F("{\n\t\"no useful message\":\""));
  Serial.print(mssg);
  Serial.println(F("\"\n}"));

}

String getCommand(String mssg) {
  if (mssg.indexOf(':') == -1) return "";
  else  return mssg.substring(mssg.indexOf('"') + 1 , mssg.lastIndexOf('"'));

}

int countValues(String mssg) {
  if (mssg.indexOf(':') == -1)return 0;
  else if ((bool)mssg.substring(mssg.indexOf(':') + 1 , mssg.lastIndexOf(',')).toInt())return 1;
  else if (int firstBracket = mssg.indexOf('[')) {
    if (mssg.indexOf(',') == -1)return 1;
    int i = 1;
    int index = 0;
    while (index = mssg.indexOf(',', index)) {
      i++;
    }
    return i;
  }
  else return 0;

}
bool validateInput(String command, int expectedArguments) {
  if ( expectedArguments == 0) return false;//always get one argument (for now)
  else {
    for (int i = 0; i < expectedArguments; i++) {
      String item = doc[command][i];
      if (item.equals("null"))return false;
    }
  }
  last_command = command;
  return true;
}
/*
  int getValue(String mssg) {
  return mssg.substring(mssg.indexOf(':') + 1 , mssg.lastIndexOf(';')).toInt();
  }*/

bool checkForParameters(String mssg, String command, int amount) { //later expandable for multiple parameters
  if (mssg.startsWith(command)) {
    if (amount == 0)  return mssg.indexOf(':') == -1;
    if (amount == 1)  return mssg.indexOf(':') != -1;
    if (amount == 2)  return mssg.indexOf(':', mssg.indexOf(':')) != -1;
    if (amount == 3)  return mssg.indexOf(':', mssg.indexOf(':', mssg.indexOf(':'))) != -1;
  } else return false;
}




float fmap(float x, float in_min, float in_max, float out_min, float out_max)
{
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}


boolean isNumeric(String str) {
  unsigned int stringLength = str.length();
  if (stringLength == 0) {
    return false;
  }
  boolean seenDecimal = false;
  for (unsigned int i = 0; i < stringLength; ++i) {
    if (isDigit(str.charAt(i))) {
      continue;
    } else if ( i == 0 && str.charAt(0) == ' -') {
      continue;
    }
    if (str.charAt(i) == '.') {
      if (seenDecimal) {
        return false;
      }
      seenDecimal = true;
      continue;
    }
    return false;
  }
  return true;
}
