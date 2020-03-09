using System;
using Input;
using Messaging.Raw;
using UnityEngine;

namespace Messaging
{
    public static class InputMessageParser
    {
        public static RawInput ParseMessage(string msg)
        {
            try
            {
                return JsonUtility.FromJson<RawInput>(msg);
            }
            catch(ArgumentException e)
            {
                Debug.LogError($"JSON error for {msg} has exception: {e.Message}\n {e.StackTrace}");
            }

            return null;
        }

        public static AllInputButtons? ParseButton(RawInput raw)
        {
            if (raw == null || raw.controllerCommand == null || raw.controllerValue == null)
            {
                Debug.LogError("ParsingButton raw is null");
                return null;
            }
            
            Debug.Log($"{raw.controllerCommand} {raw.controllerValue}");
            
            switch (raw.controllerCommand)
            {
                case "buttonKill":
                    return AllInputButtons.Kill;
                case "buttonCustomA":
                    return AllInputButtons.Custom1;
                case "buttonCustomB":
                    return AllInputButtons.Custom2;
                case "buttonCustomC":
                    return AllInputButtons.Custom3;
                case "buttonSettings":
                    return AllInputButtons.Settings;
                case "buttonThumbUp":
                    return AllInputButtons.ThumbUp;
                case "buttonThumbDown":
                    return AllInputButtons.ThumbDown;
                case "buttonYes":
                    return AllInputButtons.Yes;
                case "buttonNo":
                    return AllInputButtons.No;
                case "buttonRepeat":
                    return AllInputButtons.Repeat;
                case "buttonHorn":
                    return AllInputButtons.Horn;
                case "buttonLanguage":
                    Debug.Log($"{raw.controllerValue} {raw.controllerValue == "0"}");
                    return raw.controllerValue == "1" ? AllInputButtons.Dutch : AllInputButtons.English;
                case "buttonSlider":
                {
                    switch (raw.controllerValue)
                    {
                        case "1":
                            return AllInputButtons.One;
                        case "2":
                            return AllInputButtons.Two;
                        case "3":
                            return AllInputButtons.Three;
                        case "4":
                            return AllInputButtons.Four;
                        case "5":
                            return AllInputButtons.Five;
                    }
                    break;
                }
                // Not found
                default:
                    return null;
            }

            return null;
        }
    }
}