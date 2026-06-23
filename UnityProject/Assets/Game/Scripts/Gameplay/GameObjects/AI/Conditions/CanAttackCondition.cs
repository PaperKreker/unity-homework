using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class CanAttackCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;

        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.AttackTarget, out GameObject attackTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.ShootingDistance, out float shootingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character ||
                !attackTarget ||
                attackTarget == character ||
                attackTarget.GetComponent<HealthComponent>().IsDead
               )
            {
                return false;
            }

            Vector3 positionDelta = attackTarget.transform.position - character.transform.position;

            return positionDelta.magnitude <= shootingDistance;
        }
    }
}
