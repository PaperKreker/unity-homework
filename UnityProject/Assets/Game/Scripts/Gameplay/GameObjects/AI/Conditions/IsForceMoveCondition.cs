using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class IsForceMoveCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.ForceMove, out bool ignoreAttack))
                return false;
            
            return ignoreAttack;
        }
    }
}
