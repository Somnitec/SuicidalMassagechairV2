/*
int chair_estimated_position: 0 - 10000
enum chair_position: (up, down, neutral)
int chair_position_move_time_max: in ms  (unity settings)
int chair_position_move_time_up: in ms (unity settings)
int chair_position_move_time_down: in ms (unity settings)

bool roller_kneading_on:
int roller_kneading_speed: 0 - 255

bool roller_pounding_on:
int roller_pounding_speed: 0 - 255

bool roller_up_on:
bool roller_down_on:
bool roller_sensor_top:
bool roller_sensor_bottom:
int roller_time_up: in ms  (startup calibration)
int roller_time_down: in ms (startup calibration)
int roller_estimated_position: 0 - 10000

bool feet_roller_on:
int feet_roller_speed: 0 - 255

bool airpump_on:
bool airbag_shoulders_on:
bool airbag_arms_on:
bool airbag_legs_on:
bool airbag_outside_on:
int airbag_time_max: in ms  (unity settings)


bool butt_vibration_on:
bool backlight_on:

bool backlight_on: (last set color)
int backlight_color:  HSV ?
array backlight_LED(led, color):
array blacklight_program(program, parameters....)

enum redgreen_statuslight (red, green):

int bouncetime: in ms

stopall

ack (sends status of everything)    ->    example=    chair_estimated_position:842;chair_position:neutral;chair_position_move_time_max:1949 ..... blacklight_program:rainbow,red,blue,purple;redgreen_statuslight:red;
noack
status (sends ack)
*/
