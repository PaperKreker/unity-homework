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
                    _character.GetComponent<CharacterAI>().MoveTo(context.target);
                }
                else if (context.point != null)
                {
                    _character.GetComponent<CharacterAI>().Move(context.point.Value);
                }
            }
            else if (_next) 
                _next.Handle(ref context);
        }
    }
}