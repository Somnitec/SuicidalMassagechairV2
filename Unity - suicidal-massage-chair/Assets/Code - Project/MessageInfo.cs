using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class MessageInfo : MonoBehaviour
{
    public string Name = "Chair";
    public Messager Messager;
    public TextMeshProUGUI Text;

    void Update()
    {
        if(Messager == null || Text == null) return;
        
        Text.text = $"Connection info {Name}:\n" +
                    $"Connected: {Messager.ArduinoConnected}\n" +
                    $"Messages, received:{Messager.MessagesReceived} sent:{Messager.MessagesSent}";
    }
}
