using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public sealed class HoldPositionHandler : InputHandler
    {
        [SerializeField]
        private KeyCode _keyCode = KeyCode.H;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private InputHandler _next;

        public override void Handle(ref InputContext context)
        {
            if (Input.GetKeyDown(_keyCode))
            {
                Debug.Log($"<color=green>[Input]</color> Hold position");
                CharacterAI ai = _character.GetComponent<CharacterAI>();
                ai.Execute(new HoldCommand(), context.enqueueCommand);

            }
            else if (_next) 
                _next.Handle(ref context);
        }
    }
}