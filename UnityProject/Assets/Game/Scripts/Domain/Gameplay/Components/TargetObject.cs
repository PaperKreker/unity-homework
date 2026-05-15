using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour, ISerializableEntity
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }
        
        public string Key => "TargetObject";
        public JToken Serialize(IEntitySerializer serializer) => serializer.Serialize(this);
        public void Deserialize(IEntitySerializer serializer, JToken token) => serializer.Deserialize(this, token);
    }
}