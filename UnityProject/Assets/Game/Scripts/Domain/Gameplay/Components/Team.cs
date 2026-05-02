using SampleGame.Common;
using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour, ISaveable
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }


        public ISaveSerializer Serializer => new TeamSerializer(this);
    }
}