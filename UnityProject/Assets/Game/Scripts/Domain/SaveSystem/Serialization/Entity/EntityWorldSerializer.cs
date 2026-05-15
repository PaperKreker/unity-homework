using System.Collections;
using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using UnityEngine;
using Zenject;

namespace SampleGame.SaveSystem
{
    public class EntityWorldSerializer : ISerializer<EntityWorldSerializer.EntityData[]>
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
        
        public EntityData[] Serialize()
        {
            var entities = _entityWorld.GetAll();
            return SerializeEntities(entities);
        }
        
        public void Deserialize(EntityData[] entityContainer)
        {
            _entityWorld.DestroyAll();
            SpawnEntities(entityContainer, out var spawnedEntities);
            DeserializeEntities(spawnedEntities);
        }

        private EntityData[] SerializeEntities(IReadOnlyCollection<Entity> entities)
        {
            var entityDatas = new EntityData[entities.Count];
            int i = 0;
            
            foreach (Entity entity in entities)
            {
                ISerializableEntity[] serializables = entity.GetComponents<ISerializableEntity>();
                entityDatas[i] = new EntityData()
                {
                    Name = entity.Name,
                    Position = entity.transform.position,
                    Rotation = entity.transform.rotation,
                    Data = SerializeEntity(serializables),
                };
                ++i;
            }
            
            return entityDatas;
        }

        private JObject SerializeEntity(ISerializableEntity[] serializables)
        {
            JObject data = new();
            foreach (ISerializableEntity serializable in serializables)
            {
                data.Add(serializable.Key, serializable.Serialize(_entitySerializer));
            }

            return data;
        }
        
        private void SpawnEntities(EntityData[] entityDatas, out Dictionary<EntityData, Entity> spawnedEntities)
        {
            spawnedEntities = new();
            foreach (EntityData entityData in entityDatas)
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
                ISerializableEntity[] serializables = entity.GetComponents<ISerializableEntity>();
                DeserializeEntity(data, serializables);
            }
        }

        private void DeserializeEntity(EntityData entityData, ISerializableEntity[] serializables)
        {
            foreach (ISerializableEntity serializable in serializables)
            {
                if (entityData.Data.TryGetValue(serializable.Key, out JToken token))
                {
                    serializable.Deserialize(_entitySerializer, token);
                }
            }
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