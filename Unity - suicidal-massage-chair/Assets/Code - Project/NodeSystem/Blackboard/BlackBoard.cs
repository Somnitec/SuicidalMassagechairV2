using System;
using System.Collections;
using System.Collections.Generic;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace NodeSystem.BlackBoard
{
    [CreateAssetMenu]
    public class BlackBoard : SerializedScriptableObject
    {
        public Dictionary<string, BlackBoardValue> Values = new Dictionary<string, BlackBoardValue>();
    }

    [Serializable, InlineProperty, HideLabel, HideReferenceObjectPicker]
    public class BlackBoardValue
    {
        [HorizontalGroup(Width = 0.5f), HideLabel]
        public ValueType Type;
        [HorizontalGroup(), HideLabel]
        [ShowIf("isFloat")]
        public float Float;
        [HorizontalGroup(), HideLabel]
        [ShowIf("isBool")]
        public bool Bool;

        private bool isFloat => Type == ValueType.Float;
        private bool isBool => Type == ValueType.Bool;

        private IComparable value => isBool ? (IComparable) Bool : Float;
        
        public bool Compare(Comparator comparator, BlackBoardValue compareValue, ValueType type)
        {
            Type = type;
            compareValue.Type = type;
            
            switch (comparator)
            {
                case Comparator.LargerThan:
                    return value.CompareTo(compareValue.value) == 1;
                
                case Comparator.Equals:
                default:
                    return value.Equals(compareValue.value);
            }
        }

        public enum ValueType
        {
            Float,
            Bool
        }
    }
}

namespace NodeSystem.Nodes
{
    public class VariableComparison : BaseNode
    {
        public BlackBoard.BlackBoard bb;

        public Comparison Comparison;
        
        // [Output(dynamicPortList = true)] public List<Comparison> Comparisons = new List<Comparison>();
        [Output()] public Comparison Test;
        [Output()] public Connection Test2;
        
        protected override bool HasConnections()
        {
            return true;
        }
    }

    [Serializable, InlineProperty, HideLabel, Title("Comparison")]
    public class Comparison
    {
        [HorizontalGroup("Value1")]
        [ValueDropdown("GetKeys"), PropertyOrder(-2)]
        [HideLabel]
        public string Name;
        [HorizontalGroup("Value1")]
        [HideLabel]
        [ShowInInspector, InlineProperty, PropertyOrder(-1)]
        private BlackBoardValue value => bb.Values[Name];
        
        [HorizontalGroup("Value2")]
        [HideLabel]
        public Comparator Comparator;
        [HorizontalGroup("Value2")]
        [HideLabel]
        public BlackBoardValue CompareValue;

        [ShowInInspector]
        private bool Result => Compare();
        
        public bool Compare()
        {
            return value.Compare(Comparator, CompareValue, value.Type);
        }

        [SerializeField, ShowInInspector]
        private BlackBoard.BlackBoard bb;
        
        private IEnumerable<string> GetKeys()
        {
            return bb.Values.Keys;
        }
        
        private IEnumerable GetValues()
        {
            var list = new ValueDropdownList<BlackBoardValue>();
            bb.Values.Keys.ForEach(k =>
                list.Add(k, bb.Values[k]));
            return list;
        }
    }
    
    public enum Comparator
    {
        SmallerThan,
        SmallerOrEquals,
        LargerThan,
        LargerOrEquals,
        Equals,
        NotEquals,
    }
}