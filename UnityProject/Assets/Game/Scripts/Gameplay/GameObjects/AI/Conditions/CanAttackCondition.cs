using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class CanAttackCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;

        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject attackTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.ShootingDistance, out float shootingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character ||
                !attackTarget ||
                attackTarget == character ||
                attackTarget.GetComponent<HealthComponent>().IsDead
               )
            {
                Debug.Log("Can attack fail!");
                return false;
            }

            Vector3 positionDelta = attackTarget.transform.position - character.transform.position;

            Debug.Log(positionDelta.magnitude <= shootingDistance);
            return positionDelta.magnitude <= shootingDistance;
        }
    }
}
