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
                if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Move to target {context.target.name}");
                    _character.GetComponent<CharacterAI>().MoveToTarget(context.target);
                }
                else if (context.point != null)
                {
                    Debug.Log($"<color=green>[Input]</color> Move to point {context.point}");
                    _character.GetComponent<CharacterAI>().Move(context.point.Value);
                }
            }
            else if (_next) 
                _next.Handle(ref context);
        }
    }
}