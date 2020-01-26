using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChairMicroControllerArduino  : AbstractChairMicroController
{
    public ChairMicroControllerArduino(ChairMicroControllerState state) : base(state)
    {
    }

    protected override void Down(ChairDown args)
    {
        throw new System.NotImplementedException();
    }

    protected override void Reset(ChairReset args)
    {
        throw new System.NotImplementedException();
    }

    protected override void Up(ChairUp args)
    {
        throw new System.NotImplementedException();
    }
}
