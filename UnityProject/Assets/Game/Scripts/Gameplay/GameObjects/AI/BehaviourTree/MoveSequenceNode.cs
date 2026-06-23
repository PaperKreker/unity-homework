using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class MoveSequenceNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out Vector3 targetPosition) ||
                !_blackboard.TryGetValue(BlackBoardAPI.StoppingDistance, out float stoppingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                character == null)
            {
                return BehaviourResult.Failure;
            }

            Vector3 positionDelta = targetPosition - character.transform.position;
            
            if (positionDelta.magnitude > stoppingDistance)
            {
                Vector3 direction = positionDelta.normalized;
                character.GetComponent<MoveComponent>().MoveStep(direction, deltaTime);
                return BehaviourResult.Running;
            }

            return BehaviourResult.Success;
        }
    }
}
