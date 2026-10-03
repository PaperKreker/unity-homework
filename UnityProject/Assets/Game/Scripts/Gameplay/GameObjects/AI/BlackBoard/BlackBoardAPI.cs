using Modules.AI;
using UnityEngine;

namespace SampleGame.Ai
{
    [BlackboardAPI]
    public class BlackBoardAPI
    {
        public static readonly BlackboardValueKey<GameObject> Character = new(nameof(Character));
        public static readonly BlackboardValueKey<GameObject> Target = new(nameof(Target));
        
        public static readonly BlackboardValueKey<Vector3> TargetPosition = new(nameof(TargetPosition));
        
        public static readonly BlackboardValueKey<float> StoppingDistance = new(nameof(StoppingDistance));
        public static readonly BlackboardValueKey<float> SearchingDistance = new(nameof(SearchingDistance));
        public static readonly BlackboardValueKey<float> ShootingDistance = new(nameof(ShootingDistance));
        
        public static readonly BlackboardValueKey<bool> IsForceAttack = new(nameof(IsForceAttack));
        public static readonly BlackboardValueKey<bool> IsFollow = new(nameof(IsFollow));
    }
}