using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class AttackCommand : ICharacterCommand
    {
        private readonly Vector3? _point;
        private readonly GameObject _target;

        public AttackCommand(Vector3 point) => _point = point;
        public AttackCommand(GameObject target) => _target = target;
        
        public bool TryExecute(Blackboard blackboard)
        {
            if (_point.HasValue)
            {
                AttackPoint(blackboard, _point.Value);
                return true;
            }
            if (_target)
            {
                AttackTarget(blackboard, _target);
                return true;
            }
            return false;
        }
        
        private static void AttackPoint(Blackboard blackboard, Vector3 point)
        {
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.AttackPoint);
            blackboard.SetPrimitiveValue(BlackBoardAPI.HomePosition, point);
        }
        
        private static void AttackTarget(Blackboard blackboard, GameObject target)
        {
            if (!target)
                return;
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.AttackTarget);
            blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
    }
}