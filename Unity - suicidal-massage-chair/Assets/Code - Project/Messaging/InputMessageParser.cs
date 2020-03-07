﻿using Messaging.Raw;
using UnityEngine;

namespace Messaging
{
    public static class InputMessageParser
    {
        public static RawInput ParseMessage(string msg)
        {
            return JsonUtility.FromJson<RawInput>(msg);
        }

        public static UserInputButton? ParseButton(RawInput raw)
        {
            switch (raw.controllerCommand)
            {
                case "buttonKill":
                    return UserInputButton.Kill;
                case "buttonCustomA":
                    return UserInputButton.Custom1;
                case "buttonCustomB":
                    return UserInputButton.Custom2;
                case "buttonCustomC":
                    return UserInputButton.Custom3;
                case "buttonSettings":
                    return UserInputButton.Settings;
                case "buttonThumbUp":
                    return UserInputButton.ThumbUp;
                case "buttonThumbDown":
                    return UserInputButton.ThumbDown;
                case "buttonYes":
                    return UserInputButton.Yes;
                case "buttonNo":
                    return UserInputButton.No;
                case "buttonRepeat":
                    return UserInputButton.Repeat;
                case "buttonHorn":
                    return UserInputButton.Horn;
                case "languageSet":
                    return raw.controllerValue == "0" ? UserInputButton.Dutch : UserInputButton.English;
                case "slider":
                {
                    switch (raw.controllerValue)
                    {
                        case "1":
                            return UserInputButton.One;
                        case "2":
                            return UserInputButton.Two;
                        case "3":
                            return UserInputButton.Three;
                        case "4":
                            return UserInputButton.Four;
                        case "5":
                            return UserInputButton.Five;
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