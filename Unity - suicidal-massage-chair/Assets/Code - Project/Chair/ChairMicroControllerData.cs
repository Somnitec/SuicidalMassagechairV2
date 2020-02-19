using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ChairMicroControllerState : ScriptableObject
{
    public const int MaxSpeed = 1;

    [Range(0, 1)]
    public float chair_estimated_position;
    public bool chair_up_on;
    public bool chair_down_on;
    public bool chair_status_up { get { return chair_estimated_position <= 0.1f; } }
    public bool chair_status_down { get { return chair_estimated_position >= 0.9f; } }

    [Header("Roller")]
    public bool roller_kneading_on;
    [Range(0, MaxSpeed)]
    public float roller_kneading_speed;
    public bool roller_pounding_on;
    [Range(0, MaxSpeed)]
    public float roller_pounding_speed;
    public bool roller_up_on;
    public bool roller_down_on;
    public bool roller_sensor_top;
    public bool roller_sensor_bottom;
    [Range(0, 1)]
    public float roller_position;
    [Header("Feet Roller")]
    //public bool feet_roller;
    public bool feet_roller_on;
    [Range(0, MaxSpeed)]
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
    public Color backlight_color = Color.blue;
    public bool redgreen_statuslight_red;
    public bool redgreen_statuslight_green;
}
