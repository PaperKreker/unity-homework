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
        [SerializeField] private TargetFinderComponent _targetFinder;

        private void Awake()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.StoppingDistance, _stoppingRadius.Value);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ShootingDistance, _shootingRadius.Value);
            ResetTargetPosition();
        }

        private void FixedUpdate()
        {
            GameObject target = _targetFinder.FindTarget(targetTeam);
            if (!target)
            {
                target = _character.gameObject;
            }
            //_blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
        }

        public void Stop()
        {
            ResetFollowTarget();
            ResetTargetPosition();
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsForceAttack, true);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsFollow, false);
        }
        
        public void Move(Vector3 position)
        {
            ResetFollowTarget();
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, position);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsForceAttack, false);
        }
        
        public void MoveToTarget(GameObject target)
        {
            if (!target)
            {
                target = _character.gameObject;
            }
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsForceAttack, false);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsFollow, false);
        }
        
        public void FollowTarget(GameObject target)
        {
            if (!target)
            {
                target = _character.gameObject;
            }
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsForceAttack, false);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsFollow, true);
        }
        
        public void AttackTarget(GameObject target)
        {
            if (!target)
            {
                target = _character.gameObject;
            }
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.IsForceAttack, true);
        }

        private void ResetFollowTarget()
        {
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, _character.gameObject);
        }
        private void ResetTargetPosition()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, _character.transform.position);
        }
    }
}
