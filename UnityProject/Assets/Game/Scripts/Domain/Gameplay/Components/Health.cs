using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Health : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public int Current { get; set; } = 50;

        ///Const
        [field: SerializeField]
        public int Max { get; private set; } = 100;

        public ISaveSerializer Serializer => new HealthSerializer(this);
    }
}