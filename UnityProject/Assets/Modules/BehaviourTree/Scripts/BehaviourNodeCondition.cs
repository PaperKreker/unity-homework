using UnityEngine;

namespace Modules.AI
{
    [AddComponentMenu("AI/BehaviourTree/BehaviourNode «Condition»")]
    public sealed class BehaviourNodeCondition : BehaviourNode
    {
        [Space]
        [SerializeReference]
        private ICondition _condition;

        /*protected override BehaviourResult OnUpdate(float deltaTime) =>
            _condition != null
                ? _condition.Invoke()
                    ? BehaviourResult.Success
                    : BehaviourResult.Failure
                : BehaviourResult.Failure;*/

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            var result = _condition != null
                ? _condition.Invoke()
                    ? BehaviourResult.Success
                    : BehaviourResult.Failure
                : BehaviourResult.Failure;
            Debug.Log($"Condition {gameObject.name} is {result}");
            return result;
        }
    }
}