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

        private BlackBoardValue(float f, bool b, int i, ValueType type)
        {
            Float = f;
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
    }
}