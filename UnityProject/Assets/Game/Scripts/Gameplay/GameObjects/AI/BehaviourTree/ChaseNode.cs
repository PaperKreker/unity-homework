using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class ChaseNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject followTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out _) ||
                followTarget.Equals(character))
            {
                return BehaviourResult.Failure;
            }

            Vector3 targetPosition = followTarget.transform.position;
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, targetPosition);

            return BehaviourResult.Success;
        }
    }
}
