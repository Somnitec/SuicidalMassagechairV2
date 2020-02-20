unsigned long blinkTimer = 0;

unsigned int blinkTime = 2000;

int chair_estimated_position;
enum chair_position {up, down, neutral};
int chair_position_move_time_max;
int chair_position_move_time_up;
int chair_position_move_time_down;

bool roller_kneading_on;
int roller_kneading_speed;

bool roller_pounding_on;
int roller_pounding_speed;

bool roller_up_on;
bool roller_down_on;
bool roller_sensor_top;
bool roller_sensor_bottom;
int roller_time_up;
int roller_time_down;
int roller_estimated_position;

bool feet_roller_on;
int feet_roller_speed;

bool airpump_on;
bool airbag_shoulders_on;
bool airbag_arms_on;
bool airbag_legs_on;
bool airbag_outside_on;
int airbag_time_max;

bool butt_vibration_on;

bool backlight_on;
int backlight_color;//check which parameter would be best to use
int backlight_LED;//(led, color):
int blacklight_program;//(program, parameters....)

enum redgreen_statuslight {red, green};

int button_bounce_time;

//~~~~commands

//stop_all
//reset
//calibrate

//ack (sends status of everything)    ->    example=    chair_estimated_position:842;chair_position:neutral;chair_position_move_time_max:1949 ..... blacklight_program:rainbow,red,blue,purple;redgreen_statuslight:red;
//noack
//status (sends ack)

int led = 13;
String readString;
int maxStringLength = 64;

void setup() {
  Serial.begin(9600);
  pinMode(led, OUTPUT);
  digitalWrite(led, HIGH);
}
void loop()
{
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
      if (readString.length() > maxStringLength) {
        readString = "";//preventing buffer overflow
        Serial.println("overflow error");
      }
    }
  }
}

void receiveMessage( String message) {
  message.trim();
  if (message.startsWith("blinkTime") ) {
    if (checkForParameters(message)) {
      int value = getValue(message);
      Serial.println(value);
      blinkTime = value;
    } else incorrectMessage();
  }
  else incorrectMessage();

}

void incorrectMessage() {
  Serial.println("no usefull message, sorry");
}
int getValue(String mssg) {
  return mssg.substring(mssg.indexOf(':') + 1 , mssg.lastIndexOf(';')).toInt();
}

bool checkForParameters(String mssg) { //later expandable for multiple parameters
  return mssg.indexOf(',') != -1;
}
