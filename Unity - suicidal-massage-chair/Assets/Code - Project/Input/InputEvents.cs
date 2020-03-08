using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Framework;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using Event = Framework.Event;

namespace Input
{
    public class AllInput : Event
    {
        public AllInputButtons Input;

        public AllInput(AllInputButtons input)
        {
            Input = input;
        }
    }

    public class NormalInput : Event
    {
        public NormalInputButtons Input;

        public NormalInput(NormalInputButtons input)
        {
            Input = input;
        }
    }

    public class SpecialInput : Event
    {
        public SpecialInputButtons Input;

        public SpecialInput(SpecialInputButtons input)
        {
            Input = input;
        }
    }
}