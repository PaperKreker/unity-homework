using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class IsForceAttackCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            Debug.Log(_blackboard.TryGetValue(BlackBoardAPI.IsForceAttack, out bool isAttoa) && isAttoa);
            return _blackboard.TryGetValue(BlackBoardAPI.IsForceAttack, out bool isForceAttack) && isForceAttack;
        }
    }
}
