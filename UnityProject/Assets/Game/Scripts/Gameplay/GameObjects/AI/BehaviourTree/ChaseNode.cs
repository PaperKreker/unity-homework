using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class ChaseNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.AttackTarget, out GameObject attackTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out _) ||
                !character ||
                !attackTarget ||
                attackTarget.Equals(character))
            {
                return BehaviourResult.Failure;
            }

            Vector3 targetPosition = attackTarget.transform.position;
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, targetPosition);

            return BehaviourResult.Success;
        }
    }
}
