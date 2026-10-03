using SampleGame.Ai;
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
                CharacterAI ai = _character.GetComponent<CharacterAI>();
                PatrolCommand currentPatrol = context.enqueueCommand ? ai.CurrentCommand as PatrolCommand : null;
                if (context.point != null)
                {
                    Debug.Log($"<color=green>[Input]</color> Patrol to point {context.point}");
                    if (currentPatrol != null)
                        currentPatrol.AddWaypoint(context.point.Value);
                    else
                        ai.Execute(new PatrolCommand(context.point.Value), context.enqueueCommand);
                }
                else if (context.target != null && context.target != _character)
                {
                    Debug.Log($"<color=green>[Input]</color> Patrol to target {context.target.name}");
                    if (currentPatrol != null)
                        currentPatrol.AddWaypoint(context.target);
                    else
                        ai.Execute(new PatrolCommand(context.target), context.enqueueCommand);
                }
            }
            else if (_next)
                _next.Handle(ref context);
        }
    }
}