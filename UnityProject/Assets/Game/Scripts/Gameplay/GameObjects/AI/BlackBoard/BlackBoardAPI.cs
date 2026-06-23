using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    [BlackboardAPI]
    public class BlackBoardAPI
    {
        public static readonly BlackboardValueKey<GameObject> Character = new(nameof(Character));
        public static readonly BlackboardValueKey<GameObject> AttackTarget = new(nameof(AttackTarget));
        public static readonly BlackboardValueKey<GameObject> FollowTarget = new(nameof(FollowTarget));
        
        public static readonly BlackboardValueKey<Vector3> TargetPosition = new(nameof(TargetPosition));
        
        public static readonly BlackboardValueKey<float> StoppingDistance = new(nameof(StoppingDistance));
        public static readonly BlackboardValueKey<float> SearchingDistance = new(nameof(SearchingDistance));
        public static readonly BlackboardValueKey<float> ShootingDistance = new(nameof(ShootingDistance));
        
        public static readonly BlackboardValueKey<bool> ForceMove = new(nameof(ForceMove));
    }
}