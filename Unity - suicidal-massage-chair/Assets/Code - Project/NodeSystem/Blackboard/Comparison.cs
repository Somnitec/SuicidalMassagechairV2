using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace NodeSystem.Blackboard
{
    [Serializable, InlineProperty, HideLabel, Title("Comparison")]
    public class Comparison
    {
        [HorizontalGroup("Comparison", MinWidth = 0.4f)] [ValueDropdown("GetKeys"), PropertyOrder(-2)] [HideLabel]
        public string Name;

        [HorizontalGroup("Comparison", Width = 0.2f)]
        [HideLabel]
        [ShowInInspector, InlineProperty, PropertyOrder(-1)]
        private BlackBoardValue value => GetBlackBoardValue();
        
        [HorizontalGroup("Comparison", Width = 0.15f)] [HideLabel] [ValueDropdown("ComparatorStrings")]
        public Comparator Comparator;

        [HorizontalGroup("Comparison", Width = 0.2f)] [HideLabel]
        public BlackBoardValue CompareValue;

        [ReadOnly, PropertyOrder(10)] public BlackBoard.BlackBoard BlackBoard;

        [ShowInInspector] private bool Result => Compare();

        public bool Compare()
        {
            return Compare(value, Comparator, CompareValue);
        }

        private bool Compare(BlackBoardValue value, Comparator comparator, BlackBoardValue compareValue)
        {
            compareValue.Type = value.Type;

            switch (comparator)
            {
                case Comparator.LargerThan:
                    return CompareTo(value, compareValue) == 1;
                case Comparator.LargerOrEquals:
                    return CompareTo(value, compareValue) == -1 || Equals(value, compareValue);
                case Comparator.SmallerThan:
                    return CompareTo(value, compareValue) == -1;
                case Comparator.SmallerOrEquals:
                    return CompareTo(value, compareValue) == -1 || Equals(value, compareValue);
                case Comparator.NotEquals:
                    return !Equals(value, compareValue);
                case Comparator.Equals:
                default:
                    return Equals(value, compareValue);
            }
        }

        private int CompareTo(BlackBoardValue blackBoardValue, BlackBoardValue compareValue)
        {
            return blackBoardValue.Comparable.CompareTo(compareValue.Comparable);
        }

        private bool Equals(BlackBoardValue blackBoardValue, BlackBoardValue compareValue)
        {
            return blackBoardValue.Comparable.Equals(compareValue.Comparable);
        }
        
        private IEnumerable<string> GetKeys()
        {
            return BlackBoard.Values.Keys;
        }

        private IEnumerable ComparatorStrings = new ValueDropdownList<Comparator>()
        {
            {"==", Comparator.Equals},
            {"!=", Comparator.NotEquals},
            {">", Comparator.LargerThan},
            {">=", Comparator.LargerOrEquals},
            {"<", Comparator.SmallerThan},
            {"<=", Comparator.SmallerOrEquals},
        };
        
        private BlackBoardValue GetBlackBoardValue()
        {
            if (string.IsNullOrEmpty(Name))
                BlackBoard?.Values.First();

            return BlackBoard != null ? BlackBoard.Values[Name].Value : new BlackBoardValue();
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