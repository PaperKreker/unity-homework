using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class UpdatePatrolPointNode  : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character.TryGetComponent(out CharacterAI characterAI) ||
                characterAI.CurrentCommand is not PatrolCommand patrol)
                return BehaviourResult.Failure;

            if (!patrol.TryUpdateCurrentPoint(_blackboard))
                characterAI.CompleteCommand();

            return BehaviourResult.Success;
        }
    }
}
