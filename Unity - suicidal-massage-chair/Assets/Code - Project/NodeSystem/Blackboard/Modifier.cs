using System;
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
}