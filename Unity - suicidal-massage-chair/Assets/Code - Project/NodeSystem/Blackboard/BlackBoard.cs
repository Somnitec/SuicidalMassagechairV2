using System;
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

        public void Reset()
        {
            foreach (var v in Values)
            {
                if (v.Value.ResetOnRestart)
                {
                    v.Value.Value.Reset();
                }
            }
        }
    }

    [Serializable, InlineProperty, HideLabel, HideReferenceObjectPicker]
    public class BlackBoardTypeAndValue
    {
        [HorizontalGroup(Width = 10), HideLabel]
        public bool ResetOnRestart = true;
        [HorizontalGroup(Width = 0.5f), HideLabel]
        [OnValueChanged("UpdateType")]
        public BlackBoardValue.ValueType Type;
        
        [HorizontalGroup(), HideLabel]
        public BlackBoardValue Value = new BlackBoardValue();

        public BlackBoardTypeAndValue()
        {
        }

        public BlackBoardTypeAndValue(BlackBoardValue.ValueType type, BlackBoardValue value)
        {
            Type = type;
            Value = value;
        }

        private void UpdateType()
        {
            Value.Type = Type;
        }
    }
}
