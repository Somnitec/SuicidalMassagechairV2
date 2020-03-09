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
        state.airbag_arms_on = false;
        state.airbag_legs_on = false;
        state.airbag_outside_on = false;
        state.airbag_shoulders_on = false;
        state.airpump_on = false;
        state.backlight_color = Color.white;
        state.backlight_on = false;
        state.butt_vibration_on = false;
        
        // state.chair_down_on = false;
        // state.chair_up_on = false;
        state.chair_position_estimated = 0f;
        state.feet_roller_on = false;
        state.feet_roller_speed = 0;
        // state.redgreen_statuslight_green = false;
        // state.redgreen_statuslight_red = false;
        // state.roller_down_on = false;
        state.roller_kneading_on = false;
        state.roller_kneading_speed = 0;
        // state.roller_position = 0f;
        state.roller_pounding_on = false;
        state.roller_pounding_speed = 0;
        state.roller_sensor_bottom = false;
        state.roller_sensor_top = false;
        // state.roller_up_on = false;
    }

    protected override void RedGreenStatusLight(RedGreenStatusLight e)
    {
        state.redgreen_statuslight = e.Status;
    }

    protected override void Airbag(AirBag args)
    {
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
       state.chair_position_estimated = args.NewPosition;
    }

    protected override void AirPump(AirPump args)
    {
        state.airpump_on = args.AirPumpOn;
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        state.roller_pounding_speed = args.Speed;
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        state.roller_pounding_on = args.On;
    }

    protected override void RollerPosition(RollerPosition args)
    {
        state.roller_position_target = args.NewPosition;
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        state.roller_kneading_speed = args.Speed;
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        state.roller_kneading_on = args.On;
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        state.feet_roller_speed = args.Speed;
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        state.feet_roller_on = args.On;
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        
    }

    protected override void ButtVibration(ButtVibration args)
    {
        state.butt_vibration_on = args.On;
    }

    protected override void BackLightOn(BacklightOn args)
    {
        state.backlight_on = args.On;
    }

    protected override void BackLightColor(BackLightColor args)
    {
        state.backlight_color = args.Color;
    }
}
