using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Event = Framework.Event;

[ExecuteInEditMode]
public class ChairMicrocontrollerMessager : SingletonMonoBehavior<ChairMicrocontrollerMessager>
{
    [SerializeField] [ReadOnly] [ShowInInspector]
    private bool ArduinoConnected;
    [SerializeField, PropertyOrder(10)] [TextArea(10,20)]
    private string MessagesReceived;
    private SerialController serialController;

    // Use this for initialization
    void Start()
    {
        serialController = GetComponent<SerialController>();
    }

    [PropertySpace,Button]
    public void SendMessageToArduino(string message)
    {
        Debug.Log($"Sending to arduino: [{message}]");
        serialController.SendSerialMessage(message);
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        MessagesReceived = msg + "\n" + MessagesReceived;

        UpdateConsoleMessage();

        RawChairStatus status = ChairMessageParser.ParseMessage(msg);
        
    }

    void OnConnectionEvent(bool success)
    {
        ArduinoConnected = success;
    }

    [Button]
    public void ClearConsole()
    {
        MessagesReceived = "";

        UpdateConsoleMessage();
    }

    private void UpdateConsoleMessage()
    {
        Events.Instance.Raise(new ConsoleMessage($"Chair console: \n{MessagesReceived}"));
    }
}

public static class ChairMessageParser
{
    public static RawChairStatus ParseMessage(string msg)
    {
        return JsonUtility.FromJson<RawChairStatus>(msg);
    }

    // public static Chair
}

public class RawChairStatus
{
    // public float ChairPosition => chair_estimated_position / 10000;
    public int blinkTime=2000;
    public int chair_estimated_position=4000;
    public int chair_position=0;
    public int chair_position_move_time_max=0;
    public int chair_position_move_time_up=0;
    public int chair_position_move_time_down=0;
    public int roller_kneading_on=1;
    public int roller_kneading_speed=122;
    public int roller_pounding_on=0;
    public int roller_pounding_speed=142;
    public int roller_up_on=0;
    public int roller_down_on=0;
    public int roller_sensor_top=1;
    public int roller_sensor_bottom=1;
    public int roller_time_up=0;
    public int roller_time_down=0;
    public int roller_estimated_position=0;
    public int feet_roller_on=0;
    public int feet_roller_speed=255;
    public int airpump_on=0;
    public int airbag_shoulders_on=0;
    public int airbag_arms_on=0;
    public int airbag_legs_on=0;
    public int airbag_outside_on=0;
    public int airbag_time_max=0;
    public int butt_vibration_on=1;
    public int backlight_on=0;
    public int backlight_color=0;
    // public int[] backlight_LED= new []{0,0};
    // public int[] blacklight_program=new []{0,1,2,3};
    public int redgreen_statuslight=0;
    public int button_bounce_time=0;
    public int time_since_started=0;
    public int maxStringLength=64;
    public int ackTime=14;
}

public class UpdateChairState : Event
{
    public ChairMicroControllerState State;
}

public class ConsoleMessage : Event
{
    public string Text;

    public ConsoleMessage(string msg)
    {
        Text = msg;
    }
}