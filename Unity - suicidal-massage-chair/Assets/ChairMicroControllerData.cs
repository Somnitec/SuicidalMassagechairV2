using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerState
{
    public float chair_estimated_position;
    public bool chair_up_on;
    public bool chair_down_on;
    [Header("Roller")]
    public bool roller_kneading_on;
    public float roller_kneading_speed;
    public bool roller_pounding_on;
    public float roller_pounding_speed;
    public bool roller_up_on;
    public bool roller_down_on;
    public bool roller_top_sensor;
    public bool roller_bottom_sensor;
    [Range(0, 1)]
    public float roller_position;
    [Header("Feet Roller")]
    public bool feet_roller;
    public bool feet_roller_on;
    [Range(0, 1)]
    public float feet_roller_speed;
    [Header("Airpump")]
    public bool airpump_on;
    public bool airbag_shoulders_on;
    public bool airbag_arms_on;
    public bool airbag_legs_on;
    public bool airbag_outside_on;
    [Header("Other")]
    public bool butt_vibration_on;
    public bool backlight_on;
    public Color backlight_color;
    public bool redgreen_statuslight_red;
    public bool redgreen_statuslight_green;
}
