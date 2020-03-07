using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Event = Framework.Event;

public class InputMessageSender : MonoBehaviour
{
    private Messager _messager;
    
    void Start()
    {
        _messager = GetComponent<Messager>();
        Events.Instance.AddListener<CustomButtonTextUpdate>(SendCustomButtonText);
    }

    private void SendCustomButtonText(CustomButtonTextUpdate e)
    {
        _messager.SendMessageToArduino(MessageHelper.ToJson("customScreenA", e.CustomButtonTextA));
        _messager.SendMessageToArduino(MessageHelper.ToJson("customScreenB", e.CustomButtonTextB));
        _messager.SendMessageToArduino(MessageHelper.ToJson("customScreenC", e.CustomButtonTextC));
    }
}

public class CustomButtonTextUpdate : Event
{
    public readonly string CustomButtonTextA, CustomButtonTextB, CustomButtonTextC;

    public CustomButtonTextUpdate(string dataCustomButtonA, string dataCustomButtonB, string dataCustomButtonC)
    {
        CustomButtonTextA = dataCustomButtonA;
        CustomButtonTextB = dataCustomButtonB;
        CustomButtonTextC = dataCustomButtonC;
    }
}
