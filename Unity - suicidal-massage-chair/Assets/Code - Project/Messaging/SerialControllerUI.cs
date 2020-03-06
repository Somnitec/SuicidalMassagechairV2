using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class SerialControllerUI : MonoBehaviour
{
    public SerialController Controller;

    public TextMeshProUGUI Text;

    void Start()
    {
        Text.text = Controller.portName;
    }
    
    public void ChangeComPort(string com)
    {
        Controller.portName = com;
    }
}
