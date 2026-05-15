using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.SaveSystem;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISerializableEntity
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;
        
        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
        
        public string Key => "ProductionOrder";
        public JToken Serialize(IEntitySerializer serializer) => serializer.Serialize(this);
        public void Deserialize(IEntitySerializer serializer, JToken token) => serializer.Deserialize(this, token);
    }
}