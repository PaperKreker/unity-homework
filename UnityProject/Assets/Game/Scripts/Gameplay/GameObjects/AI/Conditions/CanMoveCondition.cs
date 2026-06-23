using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class CanMoveCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.TargetPosition, out Vector3 targetPosition) ||
                !_blackboard.TryGetValue(BlackBoardAPI.StoppingDistance, out float stoppingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character)
            {
                return false;
            }

            Vector3 positionDelta = targetPosition - character.transform.position;
            return positionDelta.magnitude > stoppingDistance;
        }
    }
}
