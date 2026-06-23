using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class FollowNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.FollowTarget, out GameObject followTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out _) ||
                !character ||
                !followTarget ||
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
