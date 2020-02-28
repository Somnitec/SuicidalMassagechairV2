using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialControllerUI : MonoBehaviour
{
    public SerialController Controller;

    public void ChangeComPort(string com)
    {
        Controller.portName = com;
    }
}
