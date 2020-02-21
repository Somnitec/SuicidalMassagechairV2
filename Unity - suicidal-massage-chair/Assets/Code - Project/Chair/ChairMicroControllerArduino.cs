using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerArduino : AbstractChairMicroController
{
    public ChairMicroControllerArduino(ChairMicroControllerState state) : base(state)
    {
    }

    protected override void Reset(ResetChair args)
    {
        send(args.Serialize());
    }

    private void send(List<string> msgs)
    {
        msgs.ForEach(msg =>
            ChairMicrocontrollerMessager.Instance.SendMessage(msg)
        );
    }

    protected override void Airbag(AirBag args)
    {
        send(args.Serialize());
    }

    protected override void ChairPosition(ChairPosition args)
    {
        send(args.Serialize());
    }

    protected override void AirPump(AirPump args)
    {
        send(args.Serialize());
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        send(args.Serialize());
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        send(args.Serialize());
    }

    protected override void RollerPosition(RollerPosition args)
    {
        send(args.Serialize());
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        send(args.Serialize());
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        send(args.Serialize());
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        send(args.Serialize());
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        send(args.Serialize());
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        send(args.Serialize());
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        send(args.Serialize());
    }

    protected override void ButtVibration(ButtVibration args)
    {
        send(args.Serialize());
    }

    protected override void BackLightOn(BacklightOn args)
    {
        send(args.Serialize());
    }

    protected override void BackLightColor(BackLightColor args)
    {
        send(args.Serialize());
    }
}