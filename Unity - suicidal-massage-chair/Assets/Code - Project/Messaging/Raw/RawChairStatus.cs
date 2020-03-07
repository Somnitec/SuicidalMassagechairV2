using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;



public class RawChairStatus
{
    // 0 ... 255
    // -1 ... 1
    // 0 ... 10,000
    // Settings
    public int maxStringLength = 64;
    public uint time_since_started = 0;
    public int ackTime = 14;
    public int blinkTime = 2000;
    public int button_bounce_time = 0; // convert ms to sec

    public int chair_position_estimated = 4000; // 0..1 [0 is straight, 10k is flat]
    public int chair_position_target = 4000; // 0..1
    public int chair_position_motor_direction = 0; // [-1 .. 1] enum {up,neutral,down}
    public int chair_position_move_time_max = 0; // convert ms to sec
    public int chair_position_move_time_up = 0; // convert ms to sec
    public int chair_position_move_time_down = 0; // convert ms to sec

    public int roller_kneading_on = 1;
    public int roller_kneading_speed = 122; // 0..1
    public int roller_pounding_on = 0;
    public int roller_pounding_speed = 142; // 0..1
    public int feet_roller_on = 0;
    public int feet_roller_speed = 255;  // 0..1

    public int roller_position_estimated = 0;// 0..10k [0 is down, 10k is up]
    public int roller_position_target = 0;// 0..10k 
    public int roller_position_motor_direction = 0;// [-1 .. 1] enum {up,neutral,down}
    public int roller_move_time_up = 0; // convert ms to sec
    public int roller_move_time_down = 0; // convert ms to sec
    public int roller_sensor_top = 1;
    public int roller_sensor_bottom = 1;

    public int airpump_on = 0;
    public int airbag_time_max = 0; // convert ms to sec
    public int airbag_shoulders_on = 0;
    public int airbag_arms_on = 0;
    public int airbag_legs_on = 0;
    public int airbag_outside_on = 0;

    public int butt_vibration_on = 1;
    public int backlight_on = 0;
    public int[] backlight_color; // convert to [0..255,g,b] to Color

    public int redgreen_statuslight = 0; // 0 red , 1 green

    // public int[] backlight_LED= new []{0,0};
    // public int[] blacklight_program=new []{0,1,2,3};
}