using System;
using System.Collections;
using System.Collections.Generic;
using NodeSystem.BlackBoard;
using NodeSystem.Nodes;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using XNode;

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
        [HorizontalGroup(Width = 0.3f), HideLabel]
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

        private bool isFloat => Type == ValueType.Float;
        private bool isBool => Type == ValueType.Bool;
        private bool isInt => Type == ValueType.Int;
        
        private IComparable value
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

        public bool Compare(Comparator comparator, BlackBoardValue compareValue, ValueType type)
        {
            Type = type;
            compareValue.Type = type;
            
            switch (comparator)
            {
                case Comparator.LargerThan:
                    return CompareTo(compareValue) == 1;
                case Comparator.LargerOrEquals:
                    return CompareTo(compareValue) == -1 || Equals(compareValue);
                case Comparator.SmallerThan:
                    return CompareTo(compareValue) == -1;
                case Comparator.SmallerOrEquals:
                    return CompareTo(compareValue) == -1 || Equals(compareValue);
                case Comparator.NotEquals:
                    return !Equals(compareValue);
                case Comparator.Equals:
                default:
                    return Equals(compareValue);
            }
        }

        private int CompareTo(BlackBoardValue compareValue)
        {
            return value.CompareTo(compareValue.value);
        }

        private bool Equals(BlackBoardValue compareValue)
        {
            return value.Equals(compareValue.value);
        }

        public enum ValueType
        {
            Float,
            Bool,
            Int
        }
    }
}

namespace NodeSystem.Nodes
{
    public class VariableComparison : BaseNode
    {
        public Comparison Comparison = new Comparison();
        
        [Output()] public Connection True;
        [Output()] public Connection False;
        
        private NodePort TruePort => GetOutputPort("True");
        private NodePort FalsePort => GetOutputPort("False");

        public override void OnNodeEnable()
        {
            if(Comparison.Compare())
                GoToNode(TruePort);
            else
                GoToNode(FalsePort);
        }

        protected override bool HasConnections()
        {
            return TruePort.IsConnected || FalsePort.IsConnected;
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
        [HideLabel] [ValueDropdown("ComparatorStrings")]
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

        private IEnumerable ComparatorStrings = new ValueDropdownList<Comparator>()
        {
            { "==", Comparator.Equals},
            { "!=", Comparator.NotEquals},
            { ">", Comparator.LargerThan},
            { ">=", Comparator.LargerOrEquals},
            { "<", Comparator.SmallerThan},
            { "<=", Comparator.SmallerOrEquals},
        };
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