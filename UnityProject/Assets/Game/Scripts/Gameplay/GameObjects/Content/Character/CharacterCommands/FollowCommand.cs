using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class FollowCommand : ICharacterCommand
    {
        private readonly GameObject _target;

        public FollowCommand(GameObject target) => _target = target;
        
        public bool TryExecute(Blackboard blackboard)
        {
            if (_target)
            {
                FollowTarget(blackboard, _target);
                return true;
            }
            return false;
        }
        
        private static void FollowTarget(Blackboard blackboard, GameObject target)
        {
            if (!target) 
                return;
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.Follow);
            blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
    }
}