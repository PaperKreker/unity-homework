using Newtonsoft.Json.Linq;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour, ISerializableEntity
    {
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField]
        public float Duration { get; private set; }

        public string Key => "Countdown";
        public JToken Serialize(IEntitySerializer serializer) => serializer.Serialize(this);
        public void Deserialize(IEntitySerializer serializer, JToken token) => serializer.Deserialize(this, token);
    }
}