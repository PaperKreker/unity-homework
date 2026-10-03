using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    public interface ICharacterCommand
    {
        public bool TryExecute(Blackboard blackboard);
    }
}