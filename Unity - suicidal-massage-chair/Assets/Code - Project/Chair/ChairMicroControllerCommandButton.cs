using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMicroControllerCommandButton : MonoBehaviour
{
    public ChairMicroController.Commands Command;

    private ChairMicroController controller;

    private void Start()
    {
        controller = FindObjectOfType<ChairMicroController>();
    }

    public void SendButtonToController()
    {
        //controller.SendCommand(Command);
    }
}
