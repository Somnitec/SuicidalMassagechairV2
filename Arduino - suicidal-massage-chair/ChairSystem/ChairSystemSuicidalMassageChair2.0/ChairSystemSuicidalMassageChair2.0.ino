#include <FastLED.h>
#include <Bounce2.h>

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

//EDITABLE VARIABLES
unsigned int blinkTime = 2000;

int chair_estimated_position;
enum positionMotors {up, down, neutral};
positionMotors chair_position_motor;
int chair_position_move_time_max;
int chair_position_move_time_up;
int chair_position_move_time_down;

bool roller_kneading_on;
int roller_kneading_speed = 255;

bool roller_pounding_on;
int roller_pounding_speed = 255;

//bool roller_up_on;
//bool roller_down_on;
Bounce roller_sensor_top = Bounce();
Bounce roller_sensor_bottom = Bounce();
int roller_time_up;
int roller_time_down;
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
int backlight_color;//check which parameter would be best to use
int backlight_LED[] = {0, 0}; //(led, color):
int blacklight_program[] = {0, 1, 2, 3}; //(program, parameters....)

enum statuslightColors {red, green};
statuslightColors redgreen_statuslight;

int button_bounce_time;

long time_since_started;

int maxStringLength = 64;

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

void setup() {
  Serial.begin(9600);
  //while (!Serial);//leonardo fix?

  pinMode(led, OUTPUT);
  digitalWrite(led, HIGH);

  for (int i = 0; i < outputAmount; i++)
    pinMode(outputs[i], OUTPUT);
  for (int i = 0; i < inputAmount; i++)
    pinMode(inputs[i], INPUT);

  FastLED.addLeds<LED_TYPE, DATA_PIN, COLOR_ORDER>(leds, NUM_LEDS).setCorrection(TypicalLEDStrip);
  FastLED.setBrightness(BRIGHTNESS);

  roller_sensor_top.attach(topstop, INPUT_PULLUP);
  roller_sensor_top.interval(button_bounce_time);
  roller_sensor_bottom.attach(botstop, INPUT_PULLUP);
  roller_sensor_bottom.interval(button_bounce_time);

  calibrationRoutine();
}
void loop()
{
  roller_sensor_top.update();
  roller_sensor_bottom.update();
  //add code for making sure the motor doesn't keep moving



  //Blinking the led to see if code is still running
  if (millis() > blinkTimer + blinkTime)
  {
    digitalWrite(led, !digitalRead(led));
    blinkTimer = millis();
  }

  if (Serial.available())  {
    char c = Serial.read();  //gets one byte from serial buffer
    if (c == '\n') {  //looks for end of data packet marker
      Serial.read(); //gets rid of following \r
      receiveMessage(readString); //prints string to serial port out
      //do stuff with captured readString
      readString = ""; //clears variable for new input
    }
    else {
      readString += c; //makes the string readString
      if (readString.length() > maxStringLength) {//preventing buffer overflow
        readString = "";
        Serial.println(F("overflow error"));
      }
    }
  }
}



void receiveMessage( String message) {
  message.trim();
  if (checkForParameters(message, F("blinkTime"), 1)) {
    blinkTime =  getValue(message);
  }

  else if (checkForParameters(message, F("chair_estimated_position"), 1)) {
    chair_estimated_position =  getValue(message);
  }
  else if (checkForParameters(message, F("chair_position_motor"), 1)) {
    chair_position_motor =  getValue(message);
  }
  else if (checkForParameters(message, F("chair_position_move_time_max"), 1)) {
    chair_position_move_time_max =  getValue(message);
  }
  else if (checkForParameters(message, F("chair_position_move_time_up"), 1)) {
    chair_position_move_time_up =  getValue(message);
  }
  else if (checkForParameters(message, F("chair_position_move_time_down"), 1)) {
    chair_position_move_time_down =  getValue(message);
  }

  else if (checkForParameters(message, F("roller_kneading_on"), 1)) {
    roller_kneading_on = getValue(message);
    analogWrite(kneading, roller_kneading_on * roller_kneading_speed);
  }
  else if (checkForParameters(message, F("roller_kneading_speed"), 1)) {
    roller_kneading_speed =  getValue(message);
  }

  else if (checkForParameters(message, F("roller_pounding_on"), 1)) {
    roller_pounding_on = getValue(message);
    analogWrite(pounding, roller_pounding_on * roller_pounding_speed);
  }
  else if (checkForParameters(message, F("roller_pounding_speed"), 1)) {
    roller_pounding_speed =  getValue(message);
  }

  else if (checkForParameters(message, F("roller_up_on"), 1)) {
    digitalWrite(mssgup, getValue(message));
  }
  else if (checkForParameters(message, F("roller_down_on"), 1)) {
    digitalWrite(mssgdown, getValue(message));
  }
  else if (checkForParameters(message, F("roller_sensor_top"), 0)) {
    //cannot be set, so it will simply return an ack
  }
  else if (checkForParameters(message, F("roller_sensor_bottom"), 0)) {
    //cannot be set, so it will simply return an ack
  }
  else if (checkForParameters(message, F("roller_time_up"), 1)) {
    roller_time_up =  getValue(message);
  }
  else if (checkForParameters(message, F("roller_time_down"), 1)) {
    roller_time_down =  getValue(message);
  }
  else if (checkForParameters(message, F("roller_estimated_position"), 1)) {
    roller_estimated_position =  getValue(message);
  }

  else if (checkForParameters(message, F("feet_roller_on"), 1)) {
    feet_roller_on = getValue(message);
    analogWrite(pounding, feet_roller_on * feet_roller_speed);
  }
  else if (checkForParameters(message, F("feet_roller_speed"), 1)) {
    feet_roller_speed =  getValue(message);
  }

  else if (checkForParameters(message, F("airpump_on"), 1)) {
    digitalWrite(pump,  getValue(message));
  }
  else if (checkForParameters(message, F("airbag_shoulders_on"), 1)) {
    digitalWrite(shoulders, getValue(message));
  }
  else if (checkForParameters(message, F("airbag_arms_on"), 1)) {
    digitalWrite(arms, getValue(message));
  }
  else if (checkForParameters(message, F("airbag_legs_on"), 1)) {
    digitalWrite(legs, getValue(message));
  }
  else if (checkForParameters(message, F("airbag_outside_on"), 1)) {
    digitalWrite(outside, getValue(message));
  }
  else if (checkForParameters(message, F("airbag_time_max"), 1)) {
    airbag_time_max =  getValue(message);
  }

  else if (checkForParameters(message, F("butt_vibration_on"), 1)) {
    digitalWrite(vibration,  getValue(message));
  }

  else if (checkForParameters(message, F("backlight_on"), 1)) {
    backlight_on = getValue(message);
  }
  else if (checkForParameters(message, F("backlight_color"), 1)) {
    backlight_color=getValue(message);
  }
  else if (checkForParameters(message, F("backlight_LED"), 2)) {
    backlight_LED[0] = getValue(message);
    //make that two parameters can be read
  }
  else if (checkForParameters(message, F("blacklight_program"), 3)) {
    blacklight_program[0] =  getValue(message);
    //make that three parameters can be read
  }

  else if (checkForParameters(message, F("redgreen_statuslight"), 1)) {
    redgreen_statuslight =  getValue(message);
  }

  else if (checkForParameters(message, F("button_bounce_time"), 1)) {
    button_bounce_time =  getValue(message);
  }

  else if (checkForParameters(message, F("time_since_started"), 1)) {
    //cannot be set, so this simply send an ack
  }

  else return incorrectMessage();

  sendAck();
}

void incorrectMessage() {
  Serial.println(F("no useful message, sorry"));

}
int getValue(String mssg) {
  return mssg.substring(mssg.indexOf(':') + 1 , mssg.lastIndexOf(';')).toInt();
}

bool checkForParameters(String mssg, String command, int amount) { //later expandable for multiple parameters
  if (mssg.startsWith(command)) {
    if (amount == 0)  return mssg.indexOf(':') == -1;
    if (amount == 1)  return mssg.indexOf(':') != -1;
    if (amount == 2)  return mssg.indexOf(':', mssg.indexOf(':')) != -1;
    if (amount == 3)  return mssg.indexOf(':', mssg.indexOf(':', mssg.indexOf(':'))) != -1;
  } else return false;
}

void sendAck() {
  unsigned long timeCheck = millis();
  //maybe make a function to test how long this took
  Serial.print(F("blinkTime:"));
  Serial.print(blinkTime);
  Serial.print(F(";chair_estimated_position:"));
  Serial.print(chair_estimated_position);
  Serial.print(F(";chair_position:"));
  Serial.print(chair_position_motor);
  Serial.print(F(";chair_position_move_time_max:"));
  Serial.print(chair_position_move_time_max);
  Serial.print(F(";chair_position_move_time_up:"));
  Serial.print(chair_position_move_time_up);
  Serial.print(F(";chair_position_move_time_down:"));
  Serial.print(chair_position_move_time_down);
  Serial.print(F(";roller_kneading_on:"));
  Serial.print(roller_kneading_on);
  Serial.print(F(";roller_kneading_speed:"));
  Serial.print(roller_kneading_speed);
  Serial.print(F(";roller_pounding_on:"));
  Serial.print(roller_pounding_on);
  Serial.print(F(";roller_pounding_speed:"));
  Serial.print(roller_pounding_speed);
  Serial.print(F(";roller_up_on:"));
  Serial.print(digitalRead(mssgup));
  Serial.print(F(";roller_down_on:"));
  Serial.print(digitalRead(mssgdown));
  Serial.print(F(";roller_sensor_top:"));
  Serial.print(roller_sensor_top.read());
  Serial.print(F(";roller_sensor_bottom:"));
  Serial.print(roller_sensor_bottom.read());
  Serial.print(F(";roller_time_up:"));
  Serial.print(roller_time_up);
  Serial.print(F(";roller_time_down:"));
  Serial.print(roller_time_down);
  Serial.print(F(";roller_estimated_position:"));
  Serial.print(roller_estimated_position);
  Serial.print(F(";feet_roller_on:"));
  Serial.print(feet_roller_on);
  Serial.print(F(";feet_roller_speed:"));
  Serial.print(feet_roller_speed);
  Serial.print(F(";airpump_on:"));
  Serial.print(digitalRead(pump));
  Serial.print(F(";airbag_shoulders_on:"));
  Serial.print(digitalRead(shoulders));
  Serial.print(F(";airbag_arms_on:"));
  Serial.print(digitalRead(arms));
  Serial.print(F(";airbag_legs_on:"));
  Serial.print(digitalRead(legs));
  Serial.print(F(";airbag_outside_on:"));
  Serial.print(digitalRead(outside));
  Serial.print(F(";airbag_time_max:"));
  Serial.print(airbag_time_max);
  Serial.print(F(";butt_vibration_on:"));
  Serial.print(digitalRead(vibration));
  Serial.print(F(";backlight_on:"));
  Serial.print(backlight_on);
  Serial.print(F(";backlight_color:"));
  Serial.print(backlight_color);
  Serial.print(F(";backlight_LED:"));
  Serial.print(backlight_LED[0]);
  Serial.print(",");
  Serial.print(backlight_LED[1]);
  Serial.print(F(";blacklight_program:"));
  Serial.print(blacklight_program[0]);
  Serial.print(",");
  Serial.print(blacklight_program[1]);
  Serial.print(",");
  Serial.print(blacklight_program[2]);
  Serial.print(",");
  Serial.print(blacklight_program[3]);
  Serial.print(F(";redgreen_statuslight:"));
  Serial.print(redgreen_statuslight);
  Serial.print(F(";button_bounce_time:"));
  Serial.print(button_bounce_time);
  Serial.print(F(";time_since_started:"));
  Serial.print(time_since_started);
  Serial.print(F(";maxStringLength:"));
  Serial.print(maxStringLength);
  Serial.print(F(";ackTime:"));
  Serial.print(millis() - timeCheck);
  Serial.print(F(";"));
  Serial.println();
}

void calibrationRoutine() {
  //to write, will move the roller up and down, counting the time that it needs.
}

float fmap(float x, float in_min, float in_max, float out_min, float out_max)
{
  return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}
