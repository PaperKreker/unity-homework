using System.Collections.Generic;
using Modules.Entities;
using SampleGame.SaveSystem;
using Zenject;

namespace SampleGame.Gameplay.Serializers
{
    public struct ProductionOrderSerializer : ISaveSerializer<ProductionOrderSerializer.Snapshot>
    {
        public string Key => "ProductionOrder";
        
        [Inject]
        public EntityCatalog Catalog;
        
        private readonly ProductionOrder _productionOrder;

        public ProductionOrderSerializer(ProductionOrder productionOrder)
        {
            _productionOrder = productionOrder;
            Catalog = null;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_productionOrder);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(Catalog, _productionOrder);
        }
        
        public struct Snapshot
        {
            public string[] Queue;

            public Snapshot(ProductionOrder productionOrder)
            {
                Queue = new string[productionOrder.Queue.Count];
                for (int i = 0; i < productionOrder.Queue.Count; ++i)
                {
                    Queue[i] = productionOrder.Queue[i].Name;
                }
                
            }

            public void Restore(EntityCatalog catalog, ProductionOrder productionOrder)
            {
                List<EntityConfig> configs = new();
                
                foreach (string key in Queue)
                {
                    if (catalog.FindConfig(key, out EntityConfig entityConfig))
                    {
                        configs.Add(entityConfig);
                    }
                }
                
                productionOrder.Queue = configs;
            }
        }
    }
}