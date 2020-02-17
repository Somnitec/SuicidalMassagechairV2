using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerMock : AbstractChairMicroController
{
    public ChairMicroControllerMock(ChairMicroControllerState state) : base(state)
    {
    }

    protected override void Reset(ResetChair args)
    {
        Debug.Log($"RESET");
        state.airbag_arms_on = false;
        state.airbag_legs_on = false;
        state.airbag_outside_on = false;
        state.airbag_shoulders_on = false;
        state.airpump_on = false;
        state.backlight_color = Color.white;
        state.backlight_on = false;
        state.butt_vibration_on = false;
        state.chair_down_on = false;
        state.chair_up_on = false;
        state.chair_estimated_position = 0f;
        state.feet_roller_on = false;
        state.feet_roller_speed = 0;
        state.redgreen_statuslight_green = false;
        state.redgreen_statuslight_red = false;
        state.roller_down_on = false;
        state.roller_kneading_on = false;
        state.roller_kneading_speed = 0;
        state.roller_position = 0f;
        state.roller_pounding_on = false;
        state.roller_pounding_speed = 0;
        state.roller_sensor_bottom = false;
        state.roller_sensor_top = false;
        state.roller_up_on = false;
    }

    protected override void Airbag(AirBag args)
    {
       Debug.Log($"AIRBAG :D {args.AirBagsOff} {args.AirBagsOn}");
    }

    protected override void ChairPosition(ChairPosition args)
    {
       Debug.Log($"ChairPosition :D {args.Duration} {args.Direction.ToString()}");
    }

    protected override void AirPump(AirPump args)
    {
        Debug.Log($"AirPump :D");
    }
}
