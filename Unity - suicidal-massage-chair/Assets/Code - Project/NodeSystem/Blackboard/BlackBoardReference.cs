using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace NodeSystem.Blackboard
{
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
            if(string.IsNullOrEmpty(Name) || !bb.Values.ContainsKey(Name))
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