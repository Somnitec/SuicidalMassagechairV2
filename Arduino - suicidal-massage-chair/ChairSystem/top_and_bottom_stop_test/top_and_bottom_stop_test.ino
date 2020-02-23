#include <Bounce2.h>

#define topstop A1
#define botstop A2
Bounce roller_sensor_top = Bounce();
Bounce roller_sensor_bottom = Bounce();
int button_bounce_time = 5;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  while (!Serial);//leonardo fix?
  roller_sensor_top.attach(topstop, INPUT);
  roller_sensor_top.interval(button_bounce_time);
  roller_sensor_bottom.attach(botstop, INPUT);
  roller_sensor_bottom.interval(button_bounce_time);
}

void loop() {
  // put your main code here, to run repeatedly:
  roller_sensor_bottom.update();
  roller_sensor_top.update();
  Serial.print(roller_sensor_bottom.read());
  Serial.print("\t");
  Serial.println(roller_sensor_top.read());
  delay(10);
}
