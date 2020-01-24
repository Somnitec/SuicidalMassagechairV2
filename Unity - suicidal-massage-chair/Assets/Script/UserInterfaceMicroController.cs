using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceMicroController : MonoBehaviour
{
    public UserInterfaceMicroControllerData data;

    public void SendButton(UserInterfaceMicroControllerData.UserInterfaceButtonValue pressed)
    {
        Debug.Log(pressed.ToString());
    }
}
