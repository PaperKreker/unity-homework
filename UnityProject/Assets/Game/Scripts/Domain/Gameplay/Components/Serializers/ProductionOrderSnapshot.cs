using System.Collections.Generic;
using Modules.Entities;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class ProductionOrderSnapshot : ISnapshot<ProductionOrder>
    {
        public string[] Queue;
        private EntityCatalog _catalog;

        public ProductionOrderSnapshot(EntityCatalog catalog)
        {
            _catalog = catalog;
        }
        
        public void Save(ProductionOrder productionOrder)
        {
            Queue = new string[productionOrder.Queue.Count];
            for (int i = 0; i < productionOrder.Queue.Count; ++i)
            {
                Queue[i] = productionOrder.Queue[i].Name;
            }
        }

        public void Restore(ProductionOrder productionOrder)
        {
            List<EntityConfig> configs = new();
                
            foreach (string key in Queue)
            {
                if (_catalog.FindConfig(key, out EntityConfig entityConfig))
                {
                    configs.Add(entityConfig);
                }
            }
                
            productionOrder.Queue = configs;
        }
    }
}