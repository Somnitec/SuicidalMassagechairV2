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
            modifier.Type = original.Type;
            
            switch (type)
            {
                case ModifyType.Set:
                    return modifier.Copy();
                case ModifyType.Add:
                {
                    switch (original.Type)
                    {
                        case BlackBoardValue.ValueType.Int:
                            var v = original.Copy();
                            v.Int = original.Int + modifier.Int;
                            return v;
                        case BlackBoardValue.ValueType.Float:
                            var v2 = original.Copy();
                            v2.Float = original.Float + modifier.Float;
                            return v2;
                        default:
                        case BlackBoardValue.ValueType.Bool:
                            break;
                    }
                }
                    break;
                case ModifyType.Multiply:
                {
                    switch (original.Type)
                    {
                        case BlackBoardValue.ValueType.Int:
                            var v = original.Copy();
                            v.Int = original.Int * modifier.Int;
                            return v;
                        case BlackBoardValue.ValueType.Float:
                            var v2 = original.Copy();
                            v2.Float = original.Float * modifier.Float;
                            return v2;
                        default:
                        case BlackBoardValue.ValueType.Bool:
                            break;
                    }
                }
                    break;
            }
            
            Debug.LogError("Something strange happened in the Modifier class");
            return original.Copy();
        }
    }

    public enum ModifyType
    {
        Set,
        Add,
        Multiply
    }
}