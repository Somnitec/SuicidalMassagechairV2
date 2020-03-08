using System.Collections;
using System.Collections.Generic;
using Framework;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

public class UserInterfaceMicroControllerCommandButton : MonoBehaviour
{
    [FormerlySerializedAs("UserInputButton")] public AllInputButtons allInputButtons;

    public void SendButtonToController()
    {
        InputSender.Send(allInputButtons);
    }
}
