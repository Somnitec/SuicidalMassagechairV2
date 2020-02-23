using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using static ChairMicroController;
using static ChairMicroController.Commands;

public class ChairButtonController : MonoBehaviour
{
    public void SendButton(Commands command)
    {
        switch (command)
        {
            case ChairDown:
                ChairPosition(1);
                break;
            case ChairUp:
                ChairPosition(0);
                break;
            case Reset:
                ResetChair();
                break;
        }
    }

    private void ResetChair()
    {
        new ResetChair().RaiseEvent();
    }

    private void ChairPosition(float v)
    {
        new ChairPosition(v).RaiseEvent();
    }
}