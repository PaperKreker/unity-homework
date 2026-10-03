using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveInputHandler : InputHandler
    {
        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private InputHandler _next;

        public override void Handle(ref InputContext context)
        {
            if (context.rightClick)
            {
                CharacterAI ai = _character.GetComponent<CharacterAI>();
                if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Move to target {context.target.name}");
                    ai.Execute(new MoveCommand(context.target), context.enqueueCommand);
                }
                else if (context.point != null)
                {
                    Debug.Log($"<color=green>[Input]</color> Move to point {context.point}");
                    ai.Execute(new MoveCommand(context.point.Value), context.enqueueCommand);
                }
            }
            else if (_next) 
                _next.Handle(ref context);
        }
    }
}