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
        
        [SerializeField] private BehaviourNodeSelector _selectorNode;
        
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
            _blackboard.SetReferenceValue(BlackBoardAPI.AttackTarget, target);
        }

        public void Stop()
        {
            ResetTargetPosition();
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ForceMove, false);
            _selectorNode.Abort();
        }
        
        public void Move(Vector3 position)
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, position);
            _blackboard.SetReferenceValue(BlackBoardAPI.FollowTarget, _character.gameObject);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ForceMove, true);
            _selectorNode.Abort();
        }
        
        public void MoveTo(GameObject target)
        {
            if (!target)
            {
                target = _character.gameObject;
            }
            _blackboard.SetReferenceValue(BlackBoardAPI.FollowTarget, target);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ForceMove, true);
            _selectorNode.Abort();
        }
        
        public void Patrol(Vector3 position)
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, position);
            _blackboard.SetPrimitiveValue(BlackBoardAPI.ForceMove, false);
            _selectorNode.Abort();
        }

        private void ResetTargetPosition()
        {
            _blackboard.SetPrimitiveValue(BlackBoardAPI.TargetPosition, _character.transform.position);
        }
    }
}
