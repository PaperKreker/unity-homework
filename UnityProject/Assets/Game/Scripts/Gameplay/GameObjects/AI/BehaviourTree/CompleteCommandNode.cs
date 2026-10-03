using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class CompleteCommandNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            if (!_blackboard.TryGetValue(BlackBoardAPI.Character, out GameObject character) ||
                !character.TryGetComponent(out CharacterAI characterAI))
                return BehaviourResult.Failure;

            characterAI.CompleteCommand();
            return BehaviourResult.Success;
        }
    }
}
