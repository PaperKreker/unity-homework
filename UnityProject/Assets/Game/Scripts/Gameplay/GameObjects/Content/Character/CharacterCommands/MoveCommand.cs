using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class MoveCommand : ICharacterCommand
    {
        private readonly Vector3? _point;
        private readonly GameObject _target;

        public MoveCommand(Vector3 point) => _point = point;
        public MoveCommand(GameObject target) => _target = target;
        
        public bool TryExecute(Blackboard blackboard)
        {
            if (_point.HasValue)
            {
                Move(blackboard, _point.Value);
                return true;
            }
            if (_target)
            {
                MoveToTarget(blackboard, _target);
                return true;
            }
            return false;
        }
        
        private static void Move(Blackboard blackboard, Vector3 position)
        {
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.Move);
            blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, position);

            GameObject character = blackboard.GetValue(BlackBoardAPI.Character);
            blackboard.SetReferenceValue(BlackBoardAPI.Target, character);
        }
        
        private static void MoveToTarget(Blackboard blackboard, GameObject target)
        {
            if (!target)
                return;
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.Move);
            blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
    }
}