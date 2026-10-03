using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class IsTargetAliveCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;

        public bool Invoke()
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject target) || !target)
                return false;
            HealthComponent healthComponent = target.GetComponent<HealthComponent>();
            return !healthComponent || healthComponent.IsAlive;
        }
    }
}
