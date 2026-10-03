using System;
using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class CharacterAI : MonoBehaviour
    {
        [SerializeField] private TeamType targetTeam;
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private Character _character;
        
        [SerializeField] private UnitRadiusComponent _stoppingRadius;
        [SerializeField] private UnitRadiusComponent _shootingRadius;
        [SerializeField] private UnitRadiusComponent _searchingRadius;
        [SerializeField] private TargetFinderComponent _targetFinder;

        private void Awake()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.StoppingDistance, _stoppingRadius.Value);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ShootingDistance, _shootingRadius.Value);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.SearchingDistance, _searchingRadius.Value);
            ResetPositions();
        }

        public void Stop()
        {
            SetCommand(BlackBoardAPI.CommandType.None);
        }
        
        public void Move(Vector3 position)
        {
            SetCommand(BlackBoardAPI.CommandType.Move);
            ResetTarget();
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, position);
        }
        
        public void MoveToTarget(GameObject target)
        {
            if (!target)
                return;
            SetCommand(BlackBoardAPI.CommandType.Move);
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
        
        public void FollowTarget(GameObject target)
        {
            if (!target) 
                return;
            SetCommand(BlackBoardAPI.CommandType.Follow);
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
        
        public void AttackTarget(GameObject target)
        {
            if (!target)
                return;
            SetCommand(BlackBoardAPI.CommandType.Attack);
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }
        
        public void HoldPosition()
        {
            SetCommand(BlackBoardAPI.CommandType.Hold);
        }
        
        public void CompleteCommand()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.None);
            ResetTarget();
            ResetPositions();
        }

        private void ResetTarget()
        {
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, _character.gameObject);
        }
        private void ResetPositions()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, _character.transform.position);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.HomePosition, _character.transform.position);
        }

        private void SetCommand(BlackBoardAPI.CommandType command)
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)command);
        }
    }
}
