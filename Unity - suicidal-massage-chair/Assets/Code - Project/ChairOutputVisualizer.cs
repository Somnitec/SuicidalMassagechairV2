using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChairOutputVisualizer : MonoBehaviour
{
    public ChairMicroControllerState data;
    public ChairOutputImages images;

    // TODO: speeds, position

    public void Update()
    {
        
        images.chair_up_on.gameObject.SetActive(data.chair_up_on);
        images.chair_down_on.gameObject.SetActive(data.chair_down_on);

        // Roller
        images.roller_kneading_on.gameObject.SetActive(data.roller_kneading_on);
        images.roller_pounding_on.gameObject.SetActive(data.roller_pounding_on);
        images.roller_up_on.gameObject.SetActive(data.roller_up_on);
        images.roller_down_on.gameObject.SetActive(data.roller_down_on);
        images.roller_sensor_top.gameObject.SetActive(data.roller_sensor_top);
        images.roller_sensor_bottom.gameObject.SetActive(data.roller_sensor_bottom);

        // Feet roller
        images.feet_roller_on.gameObject.SetActive(data.feet_roller_on);

        // Airpump
        images.airpump_on.gameObject.SetActive(data.airpump_on);
        images.airbag_shoulders_on.gameObject.SetActive(data.airbag_shoulders_on);
        images.airbag_arms_on.gameObject.SetActive(data.airbag_arms_on);
        images.airbag_legs_on.gameObject.SetActive(data.airbag_legs_on);
        images.airbag_outside_on.gameObject.SetActive(data.airbag_outside_on);

        // Other
        images.butt_vibration_on.gameObject.SetActive(data.butt_vibration_on);
        images.backlight_on.gameObject.SetActive(data.backlight_on);
        images.backlight_color.gameObject.SetActive(data.backlight_on);
        images.backlight_color.color = data.backlight_color;
        images.redgreen_statuslight_red.gameObject.SetActive(data.redgreen_statuslight_red);
        images.redgreen_statuslight_green.gameObject.SetActive(data.redgreen_statuslight_green);
    }

    [System.Serializable]
    public class ChairOutputImages
    {
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
}
