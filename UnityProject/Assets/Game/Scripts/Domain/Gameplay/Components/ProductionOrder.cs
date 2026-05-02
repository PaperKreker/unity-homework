using System.Collections.Generic;
using Modules.Entities;
using SampleGame.Gameplay.Serializers;
using SampleGame.SaveSystem;
using UnityEngine;
using Zenject;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour, ISaveable
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;
        
        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
        
        public ISaveSerializer Serializer => new ProductionOrderSerializer(this);
    }
}