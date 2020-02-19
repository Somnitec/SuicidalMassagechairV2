using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceMicroControllerCommandButton : MonoBehaviour
{
    public UserInputButton UserInputButton;

    private UserInterfaceMicroController controller;

    private void Start()
    {
        controller = FindObjectOfType<UserInterfaceMicroController>();
    }

    public void SendButtonToController()
    {
        controller.SendCommand(UserInputButton);
    }
}
