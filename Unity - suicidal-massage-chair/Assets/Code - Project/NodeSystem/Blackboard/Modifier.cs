using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeSystem.Blackboard
{
    [Serializable, InlineProperty, HideLabel, Title("Modifier")]
    public class Modifier : BlackBoardReference
    {
        [HorizontalGroup("Hor1", Width = 0.15f)] [HideLabel]
        public ModifyType Type;

        [HorizontalGroup("Hor1", Width = 0.2f)] [HideLabel]
        public BlackBoardValue ModifyingValue = new BlackBoardValue();

        [ShowInInspector] private BlackBoardValue result => Modify(false);

        public override void Init(BlackBoard.BlackBoard bb)
        {
            base.Init(bb);

            ModifyingValue.Type = value.Type;
        }

        public BlackBoardValue Modify(bool modifyOriginal = true)
        {
            var newValue = ModifyValue(value, Type, ModifyingValue);
            if (modifyOriginal)
            {
                _blackBoard.Values[Name].Value = newValue;
            }
            return newValue;
        }

        private BlackBoardValue ModifyValue(BlackBoardValue original, ModifyType type, BlackBoardValue modifier)
        {
            modifier.Type = value.Type;

            return modifier.Copy();
        }
    }

    public enum ModifyType
    {
        Set,
    }

    public class BlackBoardReference
    {
        [HorizontalGroup("Hor1", MinWidth = 0.4f)] [ValueDropdown("GetKeys"), PropertyOrder(-2)] [HideLabel]
        public string Name;

        [HorizontalGroup("Hor1", Width = 0.2f)]
        [HideLabel]
        [ShowInInspector, InlineProperty, PropertyOrder(-1)]
        protected BlackBoardValue value => GetBlackBoardValue();

        [ShowInInspector, ReadOnly]
        protected BlackBoard.BlackBoard _blackBoard;


        public virtual void Init(BlackBoard.BlackBoard bb)
        {
            _blackBoard = bb;
            Name = bb.Values.Keys.First();
        }

        private IEnumerable<string> GetKeys()
        {
            return _blackBoard.Values.Keys;
        }

        private BlackBoardValue GetBlackBoardValue()
        {
            if (string.IsNullOrEmpty(Name))
                _blackBoard?.Values.First();

            return _blackBoard != null ? _blackBoard.Values[Name].Value : new BlackBoardValue();
        }
    }
}