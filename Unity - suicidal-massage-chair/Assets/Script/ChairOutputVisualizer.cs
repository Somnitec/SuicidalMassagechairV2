using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairOutputVisualizer : MonoBehaviour
{
    public ChairMicroControllerState data;
    public Sprite chair;
    [Header("Chair Vertical Position")]
    [Range(0, 1)]
    public float chair_estimated_position;
    public Sprite chair_up_on;
    public Sprite chair_down_on;
    [Header("Roller")]
    public Sprite roller_kneading_on;
    public float roller_kneading_speed;
    public Sprite roller_pounding_on;
    public float roller_pounding_speed;
    public Sprite roller_up_on;
    public Sprite roller_down_on;
    public Sprite roller;
    public Sprite roller_top_sensor;
    public Sprite roller_bottom_sensor;
    [Range(0, 1)]
    public float roller_position;
    [Header("Feet Roller")]
    public Sprite feet_roller;
    public Sprite feet_roller_on;
    [Range(0, 1)]
    public float feet_roller_speed;
    [Header("Airpump")]
    public Sprite airpump_on;
    public Sprite airbag_shoulders_on;
    public Sprite airbag_arms_on;
    public Sprite airbag_legs_on;
    public Sprite airbag_outside_on;
    [Header("Other")]
    public Sprite butt_vibration_on;
    public Sprite backlight_on;
    public Color backlight_color;
    public Sprite redgreen_statuslight_red;
    public Sprite redgreen_statuslight_green;
   



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
