using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChairOutputVisualizer : MonoBehaviour
{
    public ChairMicroControllerState data;
    public Image chair;
    [Header("Chair Vertical Position")]
    public Image chair_up_on;
    public Image chair_down_on;
    public Image chair_status_up;
    public Image chair_status_down;
    [Header("Roller")]
    public Image roller_kneading_on;
    public Image roller_pounding_on;
    public Image roller_up_on;
    public Image roller_down_on;
    public Image roller_sensor_top;
    public Image roller_sensor_bottom;
    public Image roller_position_cursor;
    [Header("Feet Roller")]
    //public Image feet_roller; // Are we missing this?
    public Image feet_roller_on;
    [Header("Airpump")]
    public Image airpump_on;
    public Image airbag_shoulders_on;
    public Image airbag_arms_on;
    public Image airbag_legs_on;
    public Image airbag_outside_on;
    [Header("Other")]
    public Image butt_vibration_on;
    public Image backlight_on;
    public Image backlight_color;
    public Image redgreen_statuslight_red;
    public Image redgreen_statuslight_green;
}
