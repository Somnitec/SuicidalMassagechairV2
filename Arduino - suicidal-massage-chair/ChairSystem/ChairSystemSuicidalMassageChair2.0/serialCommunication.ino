void readSerial() {
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



void receiveMessage( String message) {
  last_command = "";

  DeserializationError error = deserializeJson(doc, message);
  // Test if parsing succeeds.
  if (error) {
    printError(error.c_str());
    return;
  }




  if (validateInput(F("blinkTime"), 1)) {
    blinkTime = doc["blinkTime"][0];
  }

  else if (validateInput( F("chair_position_estimated"), 1)) {
    //just ack
  }
  else if (validateInput( F("chair_position_target"), 1)) {
    chair_position_target =  doc["chair_position_target"][0];
  }
  else if (validateInput( F("chair_position_target_range"), 1)) {
    roller_position_target_range =  doc["chair_position_target_range"][0];
  }
  else if (validateInput( F("chair_position_motor_direction"), 1)) {
    chair_position_motor_direction =  doc["chair_position_motor_direction"][0];
  }
  else if (validateInput( F("chair_position_move_time_max"), 1)) {
    chair_position_move_time_max =  doc["chair_position_move_time_max"][0];
  }
  else if (validateInput( F("chair_position_move_time_up"), 1)) {
    chair_position_move_time_up =  doc["chair_position_move_time_up"][0];
  }
  else if (validateInput( F("chair_position_move_time_down"), 1)) {
    chair_position_move_time_down =  doc["chair_position_move_time_down"][0];
  }

  else if (validateInput( F("roller_kneading_on"), 1)) {
    roller_kneading_on = doc["roller_kneading_on"][0];
    analogWrite(kneading, roller_kneading_on * roller_kneading_speed);
  }
  else if (validateInput( F("roller_kneading_speed"), 1)) {
    roller_kneading_speed =  doc["roller_kneading_speed"][0];
    analogWrite(kneading, roller_kneading_on * roller_kneading_speed);
  }

  else if (validateInput( F("roller_pounding_on"), 1)) {
    roller_pounding_on = doc["roller_pounding_on"][0];
    analogWrite(pounding, roller_pounding_on * roller_pounding_speed);
  }
  else if (validateInput( F("roller_pounding_speed"), 1)) {
    roller_pounding_speed =  doc["roller_pounding_speed"][0];
    analogWrite(pounding, roller_pounding_on * roller_pounding_speed);
  }

  else if (validateInput( F("roller_position_estimated"), 1)) {
    //only ack
  }
  else if (validateInput( F("roller_position_target"), 1)) {
    roller_position_target =  doc["roller_position_target"][0];
    movingToTarget = true;
  }
  else if (validateInput( F("roller_position_target_range"), 1)) {
    roller_position_target_range =  doc["roller_position_target_range"][0];
  }
  else if (validateInput( F("roller_position_motor_direction"), 1)) {
    roller_position_motor_direction =  doc["roller_position_motor_direction"][0];
    movingToTarget = false;
  }
  else if (validateInput( F("roller_sensor_top"), 1)) {
    //cannot be set, so it will simply return an ack
  }
  else if (validateInput( F("roller_sensor_bottom"), 1)) {
    //cannot be set, so it will simply return an ack
  }
  else if (validateInput( F("roller_move_time_up"), 1)) {
    roller_move_time_up =  doc["roller_"][0];
  }
  else if (validateInput( F("roller_move_time_down"), 1)) {
    roller_move_time_down =  doc["roller_move_time_down"][0];
  }


  else if (validateInput( F("feet_roller_on"), 1)) {
    feet_roller_on = doc["feet_roller_on"][0];
    analogWrite(feet, feet_roller_on * feet_roller_speed);
  }
  else if (validateInput( F("feet_roller_speed"), 1)) {
    feet_roller_speed =  doc["feet_roller_speed"][0];
    analogWrite(feet, feet_roller_on * feet_roller_speed);
  }

  else if (validateInput( F("airpump_on"), 1)) {
    airpump_on =  doc["airpump_on"][0];
  }
  else if (validateInput( F("airbag_shoulders_on"), 1)) {
    airbag_shoulders_on = doc["airbag_shoulders_on"][0];
    shoulderTimer=0;
  }
  else if (validateInput( F("airbag_arms_on"), 1)) {
    airbag_arms_on = doc["airbag_arms_on"][0];
    armsTimer=0;
  }
  else if (validateInput( F("airbag_legs_on"), 1)) {
    airbag_legs_on = doc["airbag_legs_on"][0];
    legsTimer=0;
  }
  else if (validateInput( F("airbag_outside_on"), 1)) {
    airbag_outside_on = doc["airbag_outside_on"][0];
    outsideTimer=0;
  }
  else if (validateInput( F("airbag_time_max"), 1)) {
    airbag_time_max =  doc["airbag_time_max"][0];
  }

  else if (validateInput( F("butt_vibration_on"), 1)) {
    digitalWrite(vibration,  doc["butt_vibration_on"][0]);
  }

  else if (validateInput( F("backlight_on"), 1)) {
    backlight_on = doc["backlight_on"][0];
  }
  else if (validateInput( F("backlight_color"), 3)) {
    backlight_color[0] = doc["backlight_color"][0];//R
    backlight_color[1] = doc["backlight_color"][1];//G
    backlight_color[2] = doc["backlight_color"][2];//B
  }
  else if (validateInput( F("backlight_LED"), 4)) {
    backlight_LED[0] = doc["backlight_LED"][0];//led
    backlight_LED[1] = doc["backlight_LED"][1];//R
    backlight_LED[2] = doc["backlight_LED"][2];//G
    backlight_LED[3] = doc["backlight_LED"][3];//B
  }
  else if (validateInput( F("blacklight_program"), 4)) {
    blacklight_program[0] =  doc["blacklight_program"][0];//program
    blacklight_program[1] =  doc["blacklight_program"][1];//var1 (speed)
    blacklight_program[2] =  doc["blacklight_program"][2];//var2 (mod)
    blacklight_program[3] =  doc["blacklight_program"][3];//va32 (mod)

    //make that three parameters can be read
    if (blacklight_program[0] == 0) {
      ledBreathMin = blacklight_program[1];
      ledBreathMax = blacklight_program[2];
      breathingTime = blacklight_program[3];
    }
  }

  else if (validateInput( F("redgreen_statuslight"), 1)) {

    redgreen_statuslight =  doc["redgreen_statuslight"][0];
    digitalWrite(redgreen, redgreen_statuslight);
  }

  else if (validateInput( F("button_bounce_time"), 1)) {
    button_bounce_time =  doc["button_bounce_time"][0];
    roller_sensor_top.interval(button_bounce_time);
    roller_sensor_bottom.interval(button_bounce_time);
  }

  else if (validateInput( F("time_since_started"), 1)) {
    //cannot be set, so this simply send an ack
  }
  else if (validateInput( F("status"), 1)) {
    //cannot be set, so this simply send an ack
  }
  else if (validateInput( F("reset"), 1)) {
    reset();
  }

  else return incorrectMessage(message);

  sendAck();

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


boolean isNumeric(String str) {
  unsigned int stringLength = str.length();
  if (stringLength == 0) {
    return false;
  }
  boolean seenDecimal = false;
  for (unsigned int i = 0; i < stringLength; ++i) {
    if (isDigit(str.charAt(i))) {
      continue;
    } else if ( i == 0 && str.charAt(0) == '-') {
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
