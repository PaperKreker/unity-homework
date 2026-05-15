using System.Collections;
using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using UnityEngine;
using Zenject;

namespace SampleGame.SaveSystem
{
    public class EntityWorldSerializer : ISerializer<EntityWorldSerializer.EntityContainer>
    {
        public string Key => "Entity";
        private readonly IEntitySerializer _entitySerializer;
        private readonly EntityWorld _entityWorld;
        private readonly EntityCatalog _entityCatalog;

        public EntityWorldSerializer(
            IEntitySerializer entitySerializer, 
            EntityCatalog entityCatalog,
            EntityWorld entityWorld)
        {
            _entitySerializer = entitySerializer;
            _entityCatalog = entityCatalog;
            _entityWorld = entityWorld;
        }
        
        public EntityContainer Serialize()
        {
            var entities = _entityWorld.GetAll();
            return SerializeEntities(entities);
        }
        
        public void Deserialize(EntityContainer entityContainer)
        {
            _entityWorld.DestroyAll();
            SpawnEntities(entityContainer, out var spawnedEntities);
            DeserializeEntities(spawnedEntities);
        }

        private EntityContainer SerializeEntities(IReadOnlyCollection<Entity> entities)
        {
            EntityData[] entityDatas = new EntityData[entities.Count];
            int i = 0;
            
            foreach (Entity entity in entities)
            {
                ISerializableEntity[] saveables = entity.GetComponents<ISerializableEntity>();
                entityDatas[i] = new EntityData()
                {
                    Name = entity.Name,
                    Position = entity.transform.position,
                    Rotation = entity.transform.rotation,
                    Data = SerializeEntity(saveables),
                };
                ++i;
            }
            
            return new EntityContainer()
            {
                Entities = entityDatas,
            };
        }

        private JObject SerializeEntity(ISerializableEntity[] saveables)
        {
            JObject data = new();
            foreach (ISerializableEntity saveable in saveables)
            {
                data.Add(saveable.Key, saveable.Serialize(_entitySerializer));
            }

            return data;
        }
        
        private void SpawnEntities(EntityContainer entityContainer, out Dictionary<EntityData, Entity> spawnedEntities)
        {
            spawnedEntities = new();
            foreach (EntityData entityData in entityContainer.Entities)
            {
                if (!_entityCatalog.FindConfig(entityData.Name, out EntityConfig entityConfig))
                    continue;
                
                Entity entity = _entityWorld.Spawn(entityConfig.Name, entityData.Position, entityData.Rotation);
                spawnedEntities.Add(entityData, entity);
            }
        }

        private void DeserializeEntities(Dictionary<EntityData, Entity> spawnedEntities)
        {
            foreach (EntityData data in spawnedEntities.Keys)
            {
                Entity entity = spawnedEntities[data];
                ISerializableEntity[] saveables = entity.GetComponents<ISerializableEntity>();
                DeserializeEntity(data, saveables);
            }
        }

        private void DeserializeEntity(EntityData entityData, ISerializableEntity[] saveables)
        {
            foreach (ISerializableEntity saveable in saveables)
            {
                if (entityData.Data.TryGetValue(saveable.Key, out JToken token))
                {
                    saveable.Deserialize(_entitySerializer, token);
                }
            }
        }
        
        public struct EntityContainer
        {
            public EntityData[] Entities;
        }
        
        public struct EntityData
        {
            public string Name;
            public SerializedVector3 Position;
            public SerializedVector3 Rotation;
            public JObject Data;
        }
    }
}