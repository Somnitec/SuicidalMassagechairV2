
void receiveMessage( String message) {
  last_command = "";
  String currentCommand = "";

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
    chair_position_estimated =  doc["chair_position_estimated"][0];
  }
  else if (validateInput( F("chair_position_motor"), 1)) {
    chair_position_motor_direction =  doc["chair_position_motor"][0];
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
  }

  else if (validateInput( F("roller_pounding_on"), 1)) {
    roller_pounding_on = doc["roller_pounding_on"][0];
    analogWrite(pounding, roller_pounding_on * roller_pounding_speed);
  }
  else if (validateInput( F("roller_pounding_speed"), 1)) {
    roller_pounding_speed =  doc["roller_pounding_speed"][0];
  }

  else if (validateInput( F("roller_up_on"), 1)) {
    digitalWrite(mssgup, doc["roller_up_on"][0]);
  }
  else if (validateInput( F("roller_down_on"), 1)) {
    digitalWrite(mssgdown, doc["roller_down_on"][0]);
  }
  else if (validateInput( F("roller_sensor_top"), 0)) {
    //cannot be set, so it will simply return an ack
  }
  else if (validateInput( F("roller_sensor_bottom"), 0)) {
    //cannot be set, so it will simply return an ack
  }
  else if (validateInput( F("roller_move_time_up"), 1)) {
    roller_move_time_up =  doc["roller_"][0];
  }
  else if (validateInput( F("roller_move_time_down"), 1)) {
    roller_move_time_down =  doc["roller_move_time_down"][0];
  }
  else if (validateInput( F("roller_estimated_position"), 1)) {
    roller_estimated_position =  doc["roller_estimated_position"][0];
  }

  else if (validateInput( F("feet_roller_on"), 1)) {
    feet_roller_on = doc["feet_roller_on"][0];
    analogWrite(pounding, feet_roller_on * feet_roller_speed);
  }
  else if (validateInput( F("feet_roller_speed"), 1)) {
    feet_roller_speed =  doc["feet_roller_speed"][0];
  }

  else if (validateInput( F("airpump_on"), 1)) {
    digitalWrite(pump,  doc["airpump_on"][0]);
  }
  else if (validateInput( F("airbag_shoulders_on"), 1)) {
    digitalWrite(shoulders, doc["airbag_shoulders_on"][0]);
  }
  else if (validateInput( F("airbag_arms_on"), 1)) {
    digitalWrite(arms, doc["airbag_arms_on"][0]);
  }
  else if (validateInput( F("airbag_legs_on"), 1)) {
    digitalWrite(legs, doc["airbag_legs_on"][0]);
  }
  else if (validateInput( F("airbag_outside_on"), 1)) {
    digitalWrite(outside, doc["airbag_outside_on"][0]);
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
  else if (validateInput( F("backlight_color"), 1)) {
    backlight_color[0] = doc["backlight_color"][0];
  }
  else if (validateInput( F("backlight_LED"), 2)) {
    backlight_LED[0] = doc["backlight_LED"][0];
    //make that two parameters can be read
  }
  else if (validateInput( F("blacklight_program"), 3)) {
    blacklight_program[0] =  doc["blacklight_program"][0];
    //make that three parameters can be read
  }

  else if (validateInput( F("redgreen_statuslight"), 1)) {
    redgreen_statuslight =  doc["redgreen_statuslight"][0];
  }

  else if (validateInput( F("button_bounce_time"), 1)) {
    button_bounce_time =  doc["button_bounce_time"][0];
  }

  else if (validateInput( F("time_since_started"), 0)) {
    //cannot be set, so this simply send an ack
  }

  else return incorrectMessage(message);

  sendAck();


}
