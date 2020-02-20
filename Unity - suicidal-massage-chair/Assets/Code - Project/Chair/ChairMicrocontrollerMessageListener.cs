﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class ChairMicrocontrollerMessageListener : MonoBehaviour
{
    private SerialController serialController;

    // Use this for initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [Sirenix.OdinInspector.Button]
    public void SendMessage(string message)
    {
        serialController.SendSerialMessage(message);
    }
    // Update is called once per frame
    void Update()
    {
    }
    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        switch (name)
        {
            case "object":
                Debug.Log("staying alive: " + msg);
                break;
            default:
                Debug.Log("no response: " + msg);
                break;
        }
        

        
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}