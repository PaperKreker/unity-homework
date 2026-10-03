using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class ResetPositionNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out Vector3 _) || 
                !_blackboard.TryGetValue(BlackBoardAPI.HomePosition, out Vector3 homePosition))
                return BehaviourResult.Failure;

            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, homePosition);
            return BehaviourResult.Success;
        }
    }
}
