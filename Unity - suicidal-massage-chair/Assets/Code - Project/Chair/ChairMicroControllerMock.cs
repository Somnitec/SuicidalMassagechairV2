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
        Debug.Log($"RESET {args.Serialize()}");
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
        Debug.Log($"AIRBAG :D {args.Serialize()}");
        if( args.AirBagsOn.HasFlag(AirBagFlag.Arms)) state.airbag_arms_on = true;
        if (args.AirBagsOff.HasFlag(AirBagFlag.Arms)) state.airbag_arms_on = false;
        if (args.AirBagsOn.HasFlag(AirBagFlag.Legs)) state.airbag_legs_on = true;
        if (args.AirBagsOff.HasFlag(AirBagFlag.Legs)) state.airbag_legs_on = false;
        if (args.AirBagsOn.HasFlag(AirBagFlag.Shoulders)) state.airbag_shoulders_on = true;
        if (args.AirBagsOff.HasFlag(AirBagFlag.Shoulders)) state.airbag_shoulders_on = false;
        if (args.AirBagsOn.HasFlag(AirBagFlag.Outside)) state.airbag_outside_on = true;
        if (args.AirBagsOff.HasFlag(AirBagFlag.Outside)) state.airbag_outside_on = false;
    }

    protected override void ChairPosition(ChairPosition args)
    {
       Debug.Log($"~ ChairPosition :D {args.Serialize()}");

    }

    protected override void AirPump(AirPump args)
    {
        Debug.Log($"~ AirPump :D {args.Serialize()}");
        if (args.AirPumpOn) state.airpump_on = true;
        else state.airpump_on = false;
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        Debug.Log($"RollerPoundingSpeed{args.Serialize()}");
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        Debug.Log($"RollerPoundingOn{args.Serialize()}");
    }

    protected override void RollerPosition(RollerPosition args)
    {
        Debug.Log($"RollerPosition{args.Serialize()}");
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        Debug.Log($"RollerKneadingSpeed{args.Serialize()}");
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        Debug.Log($"RollerKneadingOn{args.Serialize()}");
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        Debug.Log($"RecalibrateChair{args.Serialize()}");
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        Debug.Log($"FeetRollingSpeed{args.Serialize()}");
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        Debug.Log($"FeetRollingOn{args.Serialize()}");
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        Debug.Log($"ChairStopAll{args.Serialize()}");
    }

    protected override void ButtVibration(ButtVibration args)
    {
        Debug.Log($"ButtVibration{args.Serialize()}");
    }

    protected override void BackLightOn(BacklightOn args)
    {
        Debug.Log($"BackLightOn{args.Serialize()}");
    }

    protected override void BackLightColor(BackLightColor args)
    {
        Debug.Log($"BackLightColor{args.Serialize()}");
    }
}
