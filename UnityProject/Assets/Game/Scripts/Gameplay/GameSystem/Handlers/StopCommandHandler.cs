using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public sealed class StopCommandHandler : InputHandler
    {
        [SerializeField]
        private KeyCode _keyCode = KeyCode.S;
        
        [SerializeField]
        private GameObject _character;
        
        [SerializeField]
        private InputHandler _next;
        
        public override void Handle(ref InputContext context)
        {
            if (Input.GetKeyDown(_keyCode))
            {
                Debug.Log($"<color=green>[Input]</color> Stop");
                _character.GetComponent<CharacterAI>().Stop();
            }
            else if (_next)
                _next.Handle(ref context);
        }
    }
}