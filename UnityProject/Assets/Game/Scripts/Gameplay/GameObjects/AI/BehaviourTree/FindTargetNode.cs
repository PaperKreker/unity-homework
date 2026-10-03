using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class FindTargetNode : BehaviourNode
    {
        [SerializeField] private TargetFinderComponent _targetFinder;
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !_blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject _))
                return BehaviourResult.Failure;

            TeamComponent teamComponent = character.GetComponent<TeamComponent>();
            if (!teamComponent)
                return BehaviourResult.Failure;
            
            GameObject target = _targetFinder.FindTarget(teamComponent);
            if (!target) 
                return BehaviourResult.Failure;
            
            _blackboard.SetReferenceValue(BlackBoardAPI.Target, target);
            return BehaviourResult.Success;
        }
    }
}