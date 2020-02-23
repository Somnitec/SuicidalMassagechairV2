void rollerRoutine() {

  roller_sensor_top.update();
  roller_sensor_bottom.update();
  //add code for making sure the motor doesn't keep moving
  if ( roller_sensor_top.rose() || roller_sensor_bottom.rose() ) {
    digitalWrite(mssgup, false);
    digitalWrite(mssgdown, false);
    roller_position_motor_direction = 0;
    //Serial.println("sensor triggered");
    sendAck();
  } else {
    if (!roller_sensor_top.read() || !roller_sensor_bottom.read()) {
      if (roller_position_motor_direction == 1) digitalWrite(mssgup, true);
      else if (roller_position_motor_direction == -1) digitalWrite(mssgdown, true);
      else {
        digitalWrite(mssgup, false);
        digitalWrite(mssgdown, false);
      }
    }
  }
}


void rollerCalibrationRoutine() {
  //to write, will move the roller up and down, counting the time that it needs.

  //move to top to always start from the same spot

  digitalWrite(mssgup, true);
  while (!roller_sensor_top.read()) {
    roller_sensor_top.update();
  }
  digitalWrite(mssgup, false);

  //move to bottom and count time

  digitalWrite(mssgdown, true);
  roller_move_time_up = millis();
  while (!roller_sensor_bottom.rose()) {
    roller_sensor_bottom.update();
  }
  digitalWrite(mssgdown, false);
  roller_move_time_up = millis() - roller_move_time_up;
  //Serial.println(roller_move_time_up); //15822/15850/15822 in test

  //move to top and count time

  digitalWrite(mssgup, true);
  roller_move_time_down = millis();
  while (!roller_sensor_top.rose()) {
    roller_sensor_top.update();
  }
  digitalWrite(mssgup, false);
  roller_move_time_down = millis() - roller_move_time_down;
  //Serial.println(roller_move_time_down); // 14740/14807/14783 in test

  last_command = "calibrated rollers";
  sendAck();

}
