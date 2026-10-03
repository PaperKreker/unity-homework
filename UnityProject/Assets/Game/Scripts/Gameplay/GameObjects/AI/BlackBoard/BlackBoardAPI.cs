using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    [BlackboardAPI]
    public class BlackBoardAPI
    {
        public enum CommandType { None, Hold, Move, Patrol, AttackTarget, Follow, AttackPoint }
        
        
        public static readonly BlackboardValueKey<int> Command = new(nameof(Command));
        
        public static readonly BlackboardValueKey<GameObject> Character = new(nameof(Character));
        public static readonly BlackboardValueKey<GameObject> Target = new(nameof(Target));
        
        public static readonly BlackboardValueKey<Vector3> TargetPosition = new(nameof(TargetPosition));
        public static readonly BlackboardValueKey<Vector3> HomePosition = new(nameof(HomePosition));
        
        public static readonly BlackboardValueKey<float> StoppingDistance = new(nameof(StoppingDistance));
        public static readonly BlackboardValueKey<float> SearchingDistance = new(nameof(SearchingDistance));
        public static readonly BlackboardValueKey<float> ShootingDistance = new(nameof(ShootingDistance));
    }
}