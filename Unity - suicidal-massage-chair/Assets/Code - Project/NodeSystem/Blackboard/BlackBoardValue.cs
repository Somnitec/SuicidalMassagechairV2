using System;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeSystem.Blackboard
{
    [Serializable, InlineProperty, HideLabel, HideReferenceObjectPicker]
    public class BlackBoardValue
    {
        [HideInInspector]
        public ValueType Type;
        [HorizontalGroup(), HideLabel]
        [ShowIf("isFloat")]
        public float Float;
        [HorizontalGroup(), HideLabel]
        [ShowIf("isBool")]
        public bool Bool;
        [HorizontalGroup(), HideLabel]
        [ShowIf("isInt")]
        public int Int;

        public BlackBoardValue()
        {
        }

        public BlackBoardValue(float value)
        {
            Float = value;
            Type = ValueType.Float;
        }
        
        public BlackBoardValue(int value)
        {
            Int = value;
            Type = ValueType.Int;
        }
        
        public BlackBoardValue(bool value)
        {
            Bool = value;
            Type = ValueType.Bool;
        }

        private BlackBoardValue(float value, bool b, int i, ValueType type)
        {
            Float = value;
            Bool = b;
            Int = i;
            Type = type;
        }

        private bool isFloat => Type == ValueType.Float;
        private bool isBool => Type == ValueType.Bool;
        private bool isInt => Type == ValueType.Int;
        
        public IComparable Comparable
        {
            get
            {
                switch (Type)
                {
                    case ValueType.Int:
                        return Int;
                    case ValueType.Float:
                        return Float;
                    default:
                    case ValueType.Bool:
                        return Bool;
                }
            }
        }
        
        public enum ValueType
        {
            Float,
            Bool,
            Int
        }

        public BlackBoardValue Copy()
        {
            return new BlackBoardValue(Float, Bool, Int, Type);
        }

        public void Reset()
        {
            Float = 0;
            Int = 0;
            Bool = false;
        }
    }
}