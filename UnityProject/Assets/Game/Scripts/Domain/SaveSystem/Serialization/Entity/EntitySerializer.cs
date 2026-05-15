using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Gameplay;
using SampleGame.Gameplay.Snapshots;

namespace SampleGame.SaveSystem
{
    public class EntitySerializer : IEntitySerializer
    {
        private readonly EntityCatalog _entityCatalog;
        private readonly EntityWorld _entityWorld;

        public EntitySerializer(EntityCatalog entityCatalog, EntityWorld entityWorld)
        {
            _entityCatalog = entityCatalog;
            _entityWorld = entityWorld;
        }
        
        private JToken Serialize<T>(T snapshot)
        {
            return JToken.FromObject(snapshot);
        }
        
        private T Deserialize<T>(JToken data)
        {
            return data.ToObject<T>();
        }
        
        public JToken Serialize(Countdown countdown)
        {
            CountdownSnapshot snapshot = new();
            snapshot.Save(countdown);
            return Serialize(snapshot);
        }
        public void Deserialize(Countdown countdown, JToken token)
        {
            Deserialize<CountdownSnapshot>(token)
                .Restore(countdown);
        }
        
        public JToken Serialize(DestinationPoint destinationPoint)
        {
            DestinationPointSnapshot snapshot = new();
            snapshot.Save(destinationPoint);
            return Serialize(snapshot);
        }
        public void Deserialize(DestinationPoint destinationPoint, JToken token)
        {
            Deserialize<DestinationPointSnapshot>(token)
                .Restore(destinationPoint);
        }

        public JToken Serialize(Health health)
        {
            HealthSnapshot snapshot = new();
            snapshot.Save(health);
            return Serialize(snapshot);
        }
        public void Deserialize(Health health, JToken token)
        {
            Deserialize<HealthSnapshot>(token)
                .Restore(health);
        }

        public JToken Serialize(ProductionOrder productionOrder)
        {
            ProductionOrderSnapshot snapshot = new();
            snapshot.Save(productionOrder);
            return Serialize(snapshot);
        }
        public void Deserialize(ProductionOrder productionOrder, JToken token)
        {
            Deserialize<ProductionOrderSnapshot>(token)
                .Restore(productionOrder, _entityCatalog);
        }

        public JToken Serialize(ResourceBag resourceBag)
        {
            ResourceBagSnapshot snapshot = new();
            snapshot.Save(resourceBag);
            return Serialize(snapshot);
        }
        public void Deserialize(ResourceBag resourceBag, JToken token)
        {
            Deserialize<ResourceBagSnapshot>(token)
                .Restore(resourceBag);
        }

        public JToken Serialize(TargetObject targetObject)
        {
            TargetObjectSnapshot snapshot = new();
            snapshot.Save(targetObject);
            return Serialize(snapshot);
        }
        public void Deserialize(TargetObject targetObject, JToken token)
        {
            Deserialize<TargetObjectSnapshot>(token)
                .Restore(targetObject, _entityWorld);
        }

        public JToken Serialize(Team team)
        {
            TeamSnapshot snapshot = new();
            snapshot.Save(team);
            return Serialize(snapshot);
        }
        public void Deserialize(Team team, JToken token)
        {
            Deserialize<TeamSnapshot>(token)
                .Restore(team);
        }
    }
}