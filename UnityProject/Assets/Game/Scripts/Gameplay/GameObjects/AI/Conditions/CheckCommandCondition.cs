using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public sealed class CheckCommandCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private BlackBoardAPI.CommandType _command;
        
        public bool Invoke()
        {
            return _blackboard.TryGetValue(BlackBoardAPI.Command, out int command) && 
                   (BlackBoardAPI.CommandType)command == _command;
        }
    }
}
