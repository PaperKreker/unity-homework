using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public class HoldCommand : ICharacterCommand
    {
        public bool TryExecute(Blackboard blackboard)
        {
            HoldPosition(blackboard);
            return true;
        }
        
        private static void HoldPosition(Blackboard blackboard)
        {
            blackboard.SetPrimitiveValue(BlackBoardAPI.Command, (int)BlackBoardAPI.CommandType.Hold);
        }
    }
}