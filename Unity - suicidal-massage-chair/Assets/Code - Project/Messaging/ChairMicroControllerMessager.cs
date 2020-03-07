using System.Collections;
using System.Collections.Generic;
using Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Event = Framework.Event;

namespace Messaging
{
    [ExecuteInEditMode]
    public class ChairMicroControllerMessager : Messager
    {
        protected override void OnMessageReceived(string message)
        {
            RawChairStatus status = ChairMessageParser.ParseMessage(message);
            Events.Instance.Raise(new ChairStateUpdate(status));
        }
    }

    public class ChairStateUpdate : Event
    {
        public RawChairStatus state { get; }

        public ChairStateUpdate(RawChairStatus state)
        {
            this.state = state;
        }
    }
}