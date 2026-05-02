using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField]
        public float Duration { get; private set; }
        
        public ISaveSerializer Serializer => new CountdownSerializer(this);
    }
}