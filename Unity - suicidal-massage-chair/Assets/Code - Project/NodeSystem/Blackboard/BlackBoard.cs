﻿using System;
using System.Collections;
using System.Collections.Generic;
using NodeSystem.Blackboard;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace NodeSystem.BlackBoard
{
    [CreateAssetMenu]
    public class BlackBoard : SerializedScriptableObject
    {
        public Dictionary<string, BlackBoardTypeAndValue> Values = new Dictionary<string, BlackBoardTypeAndValue>();
    }

    [Serializable, InlineProperty, HideLabel, HideReferenceObjectPicker]
    public class BlackBoardTypeAndValue
    {
        [HorizontalGroup(Width = 0.5f), HideLabel]
        [OnValueChanged("UpdateType")]
        public BlackBoardValue.ValueType Type;
        
        [HorizontalGroup(), HideLabel]
        public BlackBoardValue Value = new BlackBoardValue();

        private void UpdateType()
        {
            Value.Type = Type;
        }
    }
}
