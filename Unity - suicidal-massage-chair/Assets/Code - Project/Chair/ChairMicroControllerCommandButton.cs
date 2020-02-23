using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMicroControllerCommandButton : MonoBehaviour
{
    public ChairMicroController.Commands Command;

    private ChairButtonController controller;

    private void Start()
    {
        controller = FindObjectOfType<ChairButtonController>();
    }

    public void SendButtonToController()
    {
        controller.SendButton(Command);
    }
}
