using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public sealed class FollowInputHandler : InputHandler
    {
        [SerializeField]
        private KeyCode _keyCode = KeyCode.F;

        [SerializeField]
        private GameObject _character;

        [SerializeField]
        private InputHandler _next;

        public override void Handle(ref InputContext context)
        {
            if (Input.GetKey(_keyCode) && context.leftClick)
            {
                if (context.point != null)
                {
                    Debug.Log($"<color=green>[Input]</color> Follow point {context.point}");
                    _character.GetComponent<CharacterAI>().Move(context.point.Value);
                }
                else if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Follow point {context.target}");
                    _character.GetComponent<CharacterAI>().FollowTarget(context.target);
                }
            }
            else if (_next) 
                _next.Handle(ref context);
        }
    }
}