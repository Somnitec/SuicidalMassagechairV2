using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceButton : MonoBehaviour
{
    public UserInterfaceMicroControllerData.UserInterfaceButtonValue UserInterfaceButtonValue;

    private UserInterfaceMicroController controller;

    private void Start()
    {
        controller = FindObjectOfType<UserInterfaceMicroController>();
    }

    public void SendButtonToController()
    {
        controller.SendButton(UserInterfaceButtonValue);
    }
}
