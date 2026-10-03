using Modules.AI;
using SampleGame.Ai;
using UnityEngine;

namespace SampleGame
{
    public class NeedToFollowTargetCondition : ICondition
    {
        [SerializeField] private Blackboard _blackboard;
        
        public bool Invoke()
        {
            return _blackboard.TryGetValue(BlackBoardAPI.IsFollow, out bool isFollow) && isFollow;
        }
    }
}
