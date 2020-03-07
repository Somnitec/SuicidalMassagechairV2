using System.Collections;
using System.Collections.Generic;
using Framework;
using Messaging.Raw;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Event = Framework.Event;

namespace Messaging
{
    [ExecuteInEditMode]
    public class InputMicroControllerMessager : Messager
    {
        private Settings settings => SettingsHolder.Instance.Settings;

        protected override void OnMessageReceived(string message)
        {
            RawInput raw = InputMessageParser.ParseMessage(message);
            var optionalButton = InputMessageParser.ParseButton(raw);
            if (optionalButton is UserInputButton button)
            {
                // if (settings.LogDebugInfo)
                    Debug.Log($"Received input {button.ToString()}");
                Events.Instance.Raise(new UserInputUp(button));
            }
            else
            {
                var formattedMessage = $"{raw.controllerCommand} {raw.controllerValue}";
                if (settings.LogDebugInfo)
                    Debug.Log($"Received message {formattedMessage}");
                Events.Instance.Raise(new InputUpdate(formattedMessage));
            }
        }
    }

    public class InputUpdate : Event
    {
        public readonly string Message;

        public InputUpdate(string message)
        {
            Message = message;
        }
    }
}