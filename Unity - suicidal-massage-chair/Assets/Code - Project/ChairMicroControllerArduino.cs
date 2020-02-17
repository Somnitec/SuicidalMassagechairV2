using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerArduino  : AbstractChairMicroController
{
    public ChairMicroControllerArduino(ChairMicroControllerState state) : base(state)
    {
    }

    protected override void Reset(ResetChair args)
    {
        throw new System.NotImplementedException();
    }

    protected override void Airbag(AirBag args)
    {
        throw new System.NotImplementedException();
    }

    protected override void ChairPosition(ChairPosition args)
    {
        throw new System.NotImplementedException();
    }

    protected override void AirPump(AirPump args)
    {
        throw new System.NotImplementedException();
    }
}
