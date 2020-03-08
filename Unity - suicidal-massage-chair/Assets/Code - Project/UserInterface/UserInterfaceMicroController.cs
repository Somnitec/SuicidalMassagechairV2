using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class UserInterfaceMicroController : MonoBehaviour
{
    public UserInterfaceMicroControllerData data;

    public void SendCommand(AllInputButtons pressed)
    {
        Debug.Log(pressed.ToString());
    }
}
