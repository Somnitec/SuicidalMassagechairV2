using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerArduino : AbstractChairMicroController
{
    public ChairMicroControllerArduino(ChairMicroControllerState state) : base(state)
    {
        Events.Instance.AddListener<ChairStateUpdate>(StatusUpdate);
    }

    private void StatusUpdate(ChairStateUpdate e)
    {
        Debug.Log($"Message received. Updating State of Arduino.");
        ChairMessageParser.UpdateChairState(e.state, this.state);
    }

    public override void RemoveListeners()
    {
        base.RemoveListeners();
        Events.Instance.RemoveListener<ChairStateUpdate>(StatusUpdate);
    }

    protected override void Reset(ResetChair args)
    {
        send(args.SerializeToJson());
    }

    private void send(List<string> msgs)
    {
        msgs.ForEach(msg =>
            ChairMicrocontrollerMessager.Instance.SendMessageToArduino(msg)
        );
    }

    protected override void Airbag(AirBag args)
    {
        send(args.SerializeToJson());
    }

    protected override void ChairPosition(ChairPosition args)
    {
        send(args.SerializeToJson());
    }

    protected override void AirPump(AirPump args)
    {
        send(args.SerializeToJson());
    }

    protected override void RollerPoundingSpeed(RollerPoundingSpeed args)
    {
        send(args.SerializeToJson());
    }

    protected override void RollerPoundingOn(RollerPoundingOn args)
    {
        send(args.SerializeToJson());
    }

    protected override void RollerPosition(RollerPosition args)
    {
        send(args.SerializeToJson());
    }

    protected override void RollerKneadingSpeed(RollerKneadingSpeed args)
    {
        send(args.SerializeToJson());
    }

    protected override void RollerKneadingOn(RollerKneadingOn args)
    {
        send(args.SerializeToJson());
    }

    protected override void RecalibrateChair(RecalibrateChair args)
    {
        send(args.SerializeToJson());
    }

    protected override void FeetRollingSpeed(FeetRollerSpeed args)
    {
        send(args.SerializeToJson());
    }

    protected override void FeetRollingOn(FeetRollerOn args)
    {
        send(args.SerializeToJson());
    }

    protected override void ChairStopAll(ChairStopAll args)
    {
        send(args.SerializeToJson());
    }

    protected override void ButtVibration(ButtVibration args)
    {
        send(args.SerializeToJson());
    }

    protected override void BackLightOn(BacklightOn args)
    {
        send(args.SerializeToJson());
    }

    protected override void BackLightColor(BackLightColor args)
    {
        send(args.SerializeToJson());
    }
}