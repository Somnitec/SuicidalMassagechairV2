using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class ChairMicroControllerState : SerializedScriptableObject
{
    [Header("Settings")]
    public int MaxPosition= 10000;
    public int MaxSpeed = 255;

    [Header("State Values")]

    [Range(0, 1)]
    public float chair_position_estimated, chair_position_target;
    public ChairMotorDirection chair_position_motor_direction;

    public bool chair_up_on => chair_position_motor_direction == ChairMotorDirection.Up;
    public bool chair_down_on => chair_position_motor_direction == ChairMotorDirection.Down;
    public bool chair_status_up { get { return chair_position_estimated <= 0.1f; } }
    public bool chair_status_down { get { return chair_position_estimated >= 0.9f; } }
    [Min(0)]
    public float chair_position_move_time_max;
    [Min(0)]
    public float chair_position_move_time_up;
    [Min(0)]
    public float chair_position_move_time_down;

    [Header("Roller")]
    public bool roller_kneading_on;
    [Range(0, 1)]
    public float roller_kneading_speed;

    public bool roller_pounding_on;
    [Range(0, 1)]
    public float roller_pounding_speed;

    [Range(0, 1)]
    public float roller_position_estimated, roller_position_target;
    public ChairMotorDirection roller_position_motor_direction;

    public bool roller_up_on => roller_position_motor_direction == ChairMotorDirection.Up;
    public bool roller_down_on => roller_position_motor_direction == ChairMotorDirection.Down;
    public bool roller_sensor_top;
    public bool roller_sensor_bottom;

    [Header("Feet Roller")]
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
    public Color backlight_color = Color.blue;
    public StatusLight redgreen_statuslight;
    public bool statusLightGreen => redgreen_statuslight == StatusLight.Green;
    public bool statusLightRed => redgreen_statuslight == StatusLight.Red;

    public enum ChairMotorDirection
    {
        Up, Neutral, Down
    }

    public enum StatusLight
    {
        Red, Green
    }
}
