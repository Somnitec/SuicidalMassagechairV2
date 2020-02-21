using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using Event = Framework.Event;

public abstract class EventAction : Event
{
}

public class UserInputUp : EventAction
{
    public UserInputButton Button;

    public UserInputUp(UserInputButton button)
    {
        Button = button;
    }
}