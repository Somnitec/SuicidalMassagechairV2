using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ChairOutputVisualizer : MonoBehaviour
{
    public ChairMicroControllerState State;
    public ChairOutputImages images;

    public Slider chair_estimated_position_slider;
    public Slider chair_target_position_slider;
    public Slider roller_kneading_speed_slider;
    public Slider roller_pounding_speed_slider;
    public Slider feet_roller_speed_slider;
    public TMPro.TextMeshProUGUI roller_kneading_speed_text;
    public TMPro.TextMeshProUGUI roller_pounding_speed_text;
    public TMPro.TextMeshProUGUI feet_roller_speed_text;
    public TMPro.TextMeshProUGUI chair_position_target_text;
    public TMPro.TextMeshProUGUI chair_position_estimated_text;

    public int cursorMin = -6;
    public int cursorMax = 74;

    private const string KneadingText = "Kneading speed: ";
    private const string PoundingText = "Pounding speed: ";
    private const string PositionEstimatedText = "Estimated: ";
    private const string PositionTargetText = "Target: ";

    public void Update()
    {
        images.chair_up_on.gameObject.SetActive(State.chair_up_on);
        images.chair_down_on.gameObject.SetActive(State.chair_down_on);
        images.chair_status_up.gameObject.SetActive(State.chair_status_up);
        images.chair_status_down.gameObject.SetActive(State.chair_status_down);
        chair_estimated_position_slider.value = State.chair_position_estimated;
        chair_target_position_slider.value = State.chair_position_target;
        chair_position_target_text.text = PositionEstimatedText + State.chair_position_estimated;
        chair_position_estimated_text.text = PositionTargetText + State.chair_position_target;

        // Roller
        images.roller_kneading_on.gameObject.SetActive(State.roller_kneading_on);
        float roller_kneading_speed = !State.roller_kneading_on ? 0 : (float)State.roller_kneading_speed;
        roller_kneading_speed_text.text = KneadingText + roller_kneading_speed;
        roller_kneading_speed_slider.value = roller_kneading_speed;

        images.roller_pounding_on.gameObject.SetActive(State.roller_pounding_on);
        float roller_pounding_speed = !State.roller_pounding_on ? 0 : (float)State.roller_pounding_speed;
        roller_pounding_speed_text.text = PoundingText + roller_pounding_speed;
        roller_pounding_speed_slider.value = roller_pounding_speed;

        images.roller_up_on.gameObject.SetActive(State.roller_up_on);
        images.roller_down_on.gameObject.SetActive(State.roller_down_on);
        images.roller_sensor_top.gameObject.SetActive(State.roller_sensor_top);
        images.roller_sensor_bottom.gameObject.SetActive(State.roller_sensor_bottom);

        SetRollerCursorHeight(images.roller_position_estimated_cursor, State.roller_position_estimated);
        SetRollerCursorHeight(images.roller_position_target_cursor, State.roller_position_target);

        // Feet roller
        images.feet_roller_on.gameObject.SetActive(State.feet_roller_on);
        float feet_roller_speed = !State.feet_roller_on ? 0 : (float)State.feet_roller_speed;
        feet_roller_speed_text.text = KneadingText + feet_roller_speed;
        feet_roller_speed_slider.value = feet_roller_speed;

        // Airpump
        images.airpump_on.gameObject.SetActive(State.airpump_on);
        images.airbag_shoulders_on.gameObject.SetActive(State.airbag_shoulders_on);
        images.airbag_arms_on.gameObject.SetActive(State.airbag_arms_on);
        images.airbag_legs_on.gameObject.SetActive(State.airbag_legs_on);
        images.airbag_outside_on.gameObject.SetActive(State.airbag_outside_on);

        // Other
        images.butt_vibration_on.gameObject.SetActive(State.butt_vibration_on);
        images.backlight_on.gameObject.SetActive(State.backlight_on);
        images.backlight_color.gameObject.SetActive(State.backlight_on);
        images.backlight_color.color = State.backlight_color;
        images.redgreen_statuslight_red.gameObject.SetActive(State.statusLightRed);
        images.redgreen_statuslight_green.gameObject.SetActive(State.statusLightGreen);
    }

    private void SetRollerCursorHeight(Image image, float position)
    {
        if (image.rectTransform == null)
            return;
        Vector3 oldPos = image.rectTransform.localPosition;
        // no negative ranges plz
        float range = Mathf.Abs(cursorMax - cursorMin);
        // prevent division by zero
        range = Math.Abs(range) < 0.001f ? 0.1f : range;
        float y = position * range + cursorMin;
        image.rectTransform.localPosition = new Vector3(oldPos.x, y, oldPos.z);
    }

    [System.Serializable]
    public class ChairOutputImages
    {
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
        public Image roller_position_target_cursor;
        public Image roller_position_estimated_cursor;
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
