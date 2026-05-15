using System.Collections.Generic;
using Modules.Entities;

namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class ProductionOrderSnapshot
    {
        public string[] Queue;
        
        public void Save(ProductionOrder productionOrder)
        {
            Queue = new string[productionOrder.Queue.Count];
            for (int i = 0; i < productionOrder.Queue.Count; ++i)
            {
                Queue[i] = productionOrder.Queue[i].Name;
            }
        }

        public void Restore(ProductionOrder productionOrder, EntityCatalog catalog)
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