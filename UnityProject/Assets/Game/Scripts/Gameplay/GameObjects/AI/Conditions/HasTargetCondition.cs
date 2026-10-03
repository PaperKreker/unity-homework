using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class HasTargetCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            return _blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject target) &&
                   _blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) &&
                   character &&
                   target &&
                   target != character;
        }
    }
}
