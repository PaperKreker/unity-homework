using UnityEngine;

namespace SampleGame
{
    public sealed class PatrolInputHandler : InputHandler
    {
        [SerializeField]
        private KeyCode _keyCode = KeyCode.P;
        
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
                    Debug.Log($"<color=green>[Input]</color> Patrol to point {context.point}");
                    // TODO: Point destination
                }
                else if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Patrol to target {context.target.name}");
                    // TODO: Target destination
                }

                if (context.enqueueCommand)
                {
                    // TODO: If current command is patrol the add waypoint else enqueue command
                }
                else
                {
                    // TODO: Switch to patrol
                }
            }
            else if (_next)
                _next.Handle(ref context);
        }
    }
}