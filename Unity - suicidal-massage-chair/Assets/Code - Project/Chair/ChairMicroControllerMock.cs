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
        Debug.Log($"Mock: RESET {args.SerializeToString()}");
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
        Debug.Log($"Mock: AIRBAG :D {args.SerializeToString()}");
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
       Debug.Log($"Mock: ~ ChairPosition :D {args.SerializeToString()}");
       state.chair_estimated_position = args.NewPosition;

    }

    protected override void AirPump(AirPump args)
    {
        Debug.Log($"Mock: ~ AirPump :D {args.SerializeToString()}");
        state.airpump_on = args.AirPumpOn;
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        Debug.Log($"Mock: RollerPoundingSpeed {args.SerializeToString()}");
        state.roller_pounding_speed = args.Speed;
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        Debug.Log($"Mock: RollerPoundingOn {args.SerializeToString()}");
        state.roller_pounding_on = args.On;
    }

    protected override void RollerPosition(RollerPosition args)
    {
        Debug.Log($"Mock: RollerPosition {args.SerializeToString()}");
        state.roller_position = args.NewPosition;
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        Debug.Log($"Mock: RollerKneadingSpeed {args.SerializeToString()}");
        state.roller_kneading_speed = args.Speed;
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        Debug.Log($"Mock: RollerKneadingOn {args.SerializeToString()}");
        state.roller_kneading_on = args.On;
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        Debug.Log($"Mock: RecalibrateChair {args.SerializeToString()}");
        
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        Debug.Log($"Mock: FeetRollingSpeed {args.SerializeToString()}");
        state.feet_roller_speed = args.Speed;
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        Debug.Log($"Mock: FeetRollingOn {args.SerializeToString()}");
        state.feet_roller_on = args.On;
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        Debug.Log($"Mock: ChairStopAll {args.SerializeToString()}");
        
    }

    protected override void ButtVibration(ButtVibration args)
    {
        Debug.Log($"Mock: ButtVibration {args.SerializeToString()}");
        state.butt_vibration_on = args.On;
    }

    protected override void BackLightOn(BacklightOn args)
    {
        Debug.Log($"Mock: BackLightOn {args.SerializeToString()}");
        state.backlight_on = args.On;
    }

    protected override void BackLightColor(BackLightColor args)
    {
        Debug.Log($"Mock: BackLightColor {args.SerializeToString()}");
        state.backlight_color = args.Color;
    }
}
