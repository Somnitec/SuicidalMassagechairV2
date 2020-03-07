using System;
using System.Linq;
using UnityEngine;

public static class MessageHelper
{
    public static string ToJson(string param, params int[] values)
    {
        string flatValues = String.Join(",", values);
        string json = $"{{\n\t\"{param}\":[{flatValues}]\n}}";
        return json;
    }
    
    public static string ToJson(string param, params string[] values)
    {
        string[] strings = values.Select(s => $"\"{s}\"").ToArray();
        string flatValues = String.Join(",", strings);
        string json = $"{{\n\t\"{param}\":[{flatValues}]\n}}";
        return json;
    }

    public static Color ConvertToColor(int[] colorArray)
    {
        return new Color(colorArray[0], colorArray[1], colorArray[2]);
    }

    public static bool ConvertToBool(int boolInt)
    {
        return boolInt == 1;
    }

    public static float ConvertMsToSec(int rawChairPositionMoveTimeDown)
    {
        return (float)rawChairPositionMoveTimeDown / 1000f;
    }

}