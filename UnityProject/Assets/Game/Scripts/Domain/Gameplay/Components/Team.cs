using Newtonsoft.Json.Linq;
using SampleGame.Common;
using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour, ISerializableEntity
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }

        public string Key => "Team";
        public JToken Serialize(IEntitySerializer serializer) => serializer.Serialize(this);
        public void Deserialize(IEntitySerializer serializer, JToken token) => serializer.Deserialize(this, token);
    }
}