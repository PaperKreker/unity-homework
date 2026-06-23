using Modules.AI;
using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public class HasTargetToFollowCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.FollowTarget, out GameObject followTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.StoppingDistance, out float stoppingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character ||
                !followTarget ||
                followTarget == character)
            {
                return false;
            }
            
            Vector3 positionDelta = followTarget.transform.position - character.transform.position;
            return positionDelta.magnitude > stoppingDistance;
        }
    }
}
