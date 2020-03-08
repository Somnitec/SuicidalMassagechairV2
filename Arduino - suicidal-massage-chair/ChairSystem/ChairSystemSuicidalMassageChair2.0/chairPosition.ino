elapsedMillis chairPositionTimer;

long startChairPosition = 0;
int previousChairMotorDirection = 0;

void chairPositionRoutine() {

  //reset position counter
  if (previousChairMotorDirection != chair_position_motor_direction) {
    //Serial.println("direction changed");
    chairPositionTimer = 0;
    startChairPosition = chair_position_estimated;
    previousChairMotorDirection = chair_position_motor_direction;
  }
  //estimating position
  if (chair_position_motor_direction == 1) {
    chair_position_estimated = startChairPosition + map(chairPositionTimer, 0, chair_position_move_time_up, 0, 10000);
  }
  else if (chair_position_motor_direction == -1) {
    chair_position_estimated = startChairPosition - map(chairPositionTimer, 0, chair_position_move_time_down, 0, 10000);

  }

  /*if (chair_position_target == 0 || chair_position_target == 10000) {
    //if the extremes are asked, go all the way to the sensor, not working yet
    }
    else*/ if ( (chair_position_estimated > chair_position_target - chair_position_target_range  &&
                 chair_position_estimated < chair_position_target + chair_position_target_range  )
              ) {//stop when it is in range
    chair_position_motor_direction = 0;
    if (chair_position_target == 0)chair_position_estimated = 0+chair_position_target;
    if (chair_position_target == 10000)chair_position_estimated = 10000-chair_position_target;
    //Serial.println("positionReached");
    //found position
  }
  else if (chair_position_target < chair_position_estimated && chair_position_motor_direction != -1) {
    chair_position_motor_direction = -1;
    //chair should move down
  }
  else if (chair_position_target > chair_position_estimated && chair_position_motor_direction != 1) {
    chair_position_motor_direction = 1;
    //chair should move down
  }

  //setting motors
  if (chair_position_motor_direction == 1) {
    digitalWrite(chairdown, LOW);
    digitalWrite(chairup, HIGH);
  } else if (chair_position_motor_direction == -1) {
    digitalWrite(chairup, LOW);
    digitalWrite(chairdown, HIGH);
  } else {
    digitalWrite(chairup, LOW);
    digitalWrite(chairdown, LOW);
  }


}

void moveChairUp() {
  chairPositionTimer = 0;
  digitalWrite(chairdown, LOW);
  digitalWrite(chairup, HIGH);
  while (chairPositionTimer < chair_position_move_time_up) {}
  digitalWrite(chairup, LOW);
  digitalWrite(chairdown, LOW);
  
  chair_position_estimated=10000;
  chair_position_target=10000;
}
