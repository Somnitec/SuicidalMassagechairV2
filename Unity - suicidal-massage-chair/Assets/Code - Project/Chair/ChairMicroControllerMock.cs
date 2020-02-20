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
        Debug.Log($"RESET {args.SerializeToString()}");
        // state.airbag_arms_on = false;
        // state.airbag_legs_on = false;
        // state.airbag_outside_on = false;
        // state.airbag_shoulders_on = false;
        // state.airpump_on = false;
        // state.backlight_color = Color.white;
        // state.backlight_on = false;
        // state.butt_vibration_on = false;
        // state.chair_down_on = false;
        // state.chair_up_on = false;
        // state.chair_estimated_position = 0f;
        // state.feet_roller_on = false;
        // state.feet_roller_speed = 0;
        // state.redgreen_statuslight_green = false;
        // state.redgreen_statuslight_red = false;
        // state.roller_down_on = false;
        // state.roller_kneading_on = false;
        // state.roller_kneading_speed = 0;
        // state.roller_position = 0f;
        // state.roller_pounding_on = false;
        // state.roller_pounding_speed = 0;
        // state.roller_sensor_bottom = false;
        // state.roller_sensor_top = false;
        // state.roller_up_on = false;
    }

    protected override void Airbag(AirBag args)
    {
        Debug.Log($"AIRBAG :D {args.SerializeToString()}");
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
       Debug.Log($"~ ChairPosition :D {args.SerializeToString()}");

    }

    protected override void AirPump(AirPump args)
    {
        Debug.Log($"~ AirPump :D {args.SerializeToString()}");
        if (args.AirPumpOn) state.airpump_on = true;
        else state.airpump_on = false;
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        Debug.Log($"RollerPoundingSpeed {args.SerializeToString()}");
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        Debug.Log($"RollerPoundingOn {args.SerializeToString()}");
    }

    protected override void RollerPosition(RollerPosition args)
    {
        Debug.Log($"RollerPosition {args.SerializeToString()}");
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        Debug.Log($"RollerKneadingSpeed {args.SerializeToString()}");
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        Debug.Log($"RollerKneadingOn {args.SerializeToString()}");
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        Debug.Log($"RecalibrateChair {args.SerializeToString()}");
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        Debug.Log($"FeetRollingSpeed {args.SerializeToString()}");
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        Debug.Log($"FeetRollingOn {args.SerializeToString()}");
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        Debug.Log($"ChairStopAll {args.SerializeToString()}");
    }

    protected override void ButtVibration(ButtVibration args)
    {
        Debug.Log($"ButtVibration {args.SerializeToString()}");
    }

    protected override void BackLightOn(BacklightOn args)
    {
        Debug.Log($"BackLightOn {args.SerializeToString()}");
    }

    protected override void BackLightColor(BackLightColor args)
    {
        Debug.Log($"BackLightColor {args.SerializeToString()}");
    }
}
