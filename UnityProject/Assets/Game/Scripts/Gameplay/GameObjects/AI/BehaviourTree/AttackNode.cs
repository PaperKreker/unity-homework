using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class AttackNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.AttackTarget, out GameObject attackTarget) ||
                !_blackboard.TryGetValue(BlackBoardAPI.ShootingDistance, out float shootingDistance) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character ||
                !attackTarget || 
                attackTarget == character
                )
            {
                return BehaviourResult.Failure;
            }

            Vector3 positionDelta = attackTarget.transform.position - character.transform.position;

            if (attackTarget.GetComponent<HealthComponent>().IsDead)
            {
                return BehaviourResult.Success;
            }
            
            if (positionDelta.magnitude <= shootingDistance)
            {
                character.GetComponent<AttackComponent>().Attack(attackTarget);
                return BehaviourResult.Running;
            }

            return BehaviourResult.Failure;
        }
    }
}
