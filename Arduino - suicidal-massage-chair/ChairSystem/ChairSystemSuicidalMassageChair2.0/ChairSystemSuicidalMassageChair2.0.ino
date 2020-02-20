unsigned long blinkTimer = 0;

unsigned int blinkTime = 2000;

int chair_estimated_position;
enum positions {up, down, neutral};
positions chair_position;
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

enum statuslightColors {red, green};
statuslightColors redgreen_statuslight;

int button_bounce_time;

long time_since_started;

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
      if (readString.length() > maxStringLength) {//preventing buffer overflow
        readString = "";
        Serial.println(F("overflow error"));
      }
    }
  }
}

void receiveMessage( String message) {
  message.trim();
  if (message.startsWith("blinkTime") ) {
    if (checkForParameters(message)) {
      int value = getValue(message);
      blinkTime = value;
    } else return incorrectMessage();
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

bool checkForParameters(String mssg) { //later expandable for multiple parameters
  return mssg.indexOf(':') != -1;
}

void sendAck() {
  Serial.print(F("blinkTime:"));
  Serial.print(blinkTime);
  Serial.print(F(";chair_estimated_position:"));
  Serial.print(chair_estimated_position);
  Serial.print(F(";chair_position:"));
  Serial.print(chair_position);
  Serial.print(F(";chair_position_move_time_max:"));
  Serial.print(chair_position_move_time_max);
  Serial.print(F(";chair_position_move_time_up:"));
  Serial.print(chair_position_move_time_up);
  Serial.print(F(";chair_position_move_time_down:"));
  Serial.print(chair_position_move_time_down);
  Serial.print(F(";roller_kneading_on:"));
  Serial.print(roller_kneading_on);
  Serial.print(F(";roller_pounding_on:"));
  Serial.print(roller_pounding_on);
  Serial.print(F(";roller_pounding_speed:"));
  Serial.print(roller_pounding_speed);
  Serial.print(F(";roller_up_on:"));
  Serial.print(roller_up_on);
  Serial.print(F(";roller_down_on:"));
  Serial.print(roller_down_on);
  Serial.print(F(";roller_sensor_top:"));
  Serial.print(roller_sensor_top);
  Serial.print(F(";roller_sensor_bottom:"));
  Serial.print(roller_sensor_bottom);
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
  Serial.print(airpump_on);
  Serial.print(F(";airbag_shoulders_on:"));
  Serial.print(airbag_shoulders_on);
  Serial.print(F(";airbag_arms_on:"));
  Serial.print(airbag_arms_on);
  Serial.print(F(";airbag_legs_on:"));
  Serial.print(airbag_legs_on);
  Serial.print(F(";airbag_outside_on:"));
  Serial.print(airbag_outside_on);
  Serial.print(F(";airbag_time_max:"));
  Serial.print(airbag_time_max);
  Serial.print(F(";butt_vibration_on:"));
  Serial.print(butt_vibration_on);
  Serial.print(F(";backlight_on:"));
  Serial.print(backlight_on);
  Serial.print(F(";backlight_color:"));
  Serial.print(backlight_color);
  Serial.print(F(";backlight_LED:"));
  Serial.print(backlight_LED);
  Serial.print(F(";blacklight_program:"));
  Serial.print(blacklight_program);
  Serial.print(F(";redgreen_statuslight:"));
  Serial.print(redgreen_statuslight);
  Serial.print(F(";button_bounce_time:"));
  Serial.print(button_bounce_time);
  Serial.print(F(";time_since_started:"));
  Serial.print(time_since_started);
  Serial.println();
}
