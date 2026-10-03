using Modules.AI;
using SampleGame.Ai;
using UnityEngine;

public class ResetTargetNode : BehaviourNode
{
    [SerializeField] private Blackboard _blackboard;

    protected override BehaviourResult OnUpdate(float deltaTime)
    {
        if (!_blackboard.TryGetValue(BlackBoardAPI.Target, out GameObject followTarget) ||
            !_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
            !character ||
            !followTarget)
        {
            return BehaviourResult.Failure;
        }
        
        _blackboard.SetReferenceValue(BlackBoardAPI.Target, character);

        return BehaviourResult.Success;
    }
}
