using Messaging.Raw;
using UnityEngine;
using static ChairMicroControllerState;
using static MessageHelper;

public static class InputMessageParser
{
    public static RawInput ParseMessage(string msg)
    {
        return JsonUtility.FromJson<RawInput>(msg);
    }

    public static void UpdateChairState(RawInput raw, ChairMicroControllerState state)
    {
        // state.time_since_started = raw.time_since_started;
        // state.roller_sensor_top = ConvertToBool(raw.roller_sensor_top);
    }
}