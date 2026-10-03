using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public sealed class AttackInputHandler : InputHandler
    {
        [SerializeField]
        private KeyCode _keyCode = KeyCode.A;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private InputHandler _next;

        public override void Handle(ref InputContext context)
        {
            if (Input.GetKey(_keyCode) && context.leftClick)
            {
                CharacterAI ai = _character.GetComponent<CharacterAI>();
                if (context.point != null)
                {
                    Debug.Log($"<color=green>[Input]</color> Attack position {context.point}");
                    ai.Execute(new AttackCommand(context.point.Value), context.enqueueCommand);
                }
                else if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Attack target {context.target.name}");
                    ai.Execute(new AttackCommand(context.target), context.enqueueCommand);
                }
            }
            else if (_next)
                _next.Handle(ref context);
        }
    }
}