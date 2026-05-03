using System.Collections;
using System.Collections.Generic;
using Modules.Entities;
using Newtonsoft.Json.Linq;
using SampleGame.Common;
using UnityEngine;
using Zenject;

namespace SampleGame.SaveSystem
{
    public class EntitySerializer : ISaveSerializer<EntitySerializer.EntityContainer>
    {
        public string Key => "Entity";
        private readonly DiContainer _container;
        private readonly EntityWorld _entityWorld;
        private readonly EntityCatalog _entityCatalog;

        public EntitySerializer(DiContainer container, EntityWorld entityWorld, EntityCatalog entityCatalog)
        {
            _container = container;
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
                ISaveable[] saveables = entity.GetComponents<ISaveable>();
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

        private JObject SerializeEntity(ISaveable[] saveables)
        {
            JObject data = new ();
            for (int j = 0; j < saveables.Length; ++j)
            {
                ISaveSerializer serializer = saveables[j].Serializer;
                data.Add(serializer.Key, serializer.Serialize());
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
                ISaveable[] saveables = entity.GetComponents<ISaveable>();
                DeserializeEntity(data, saveables);
            }
        }

        private void DeserializeEntity(EntityData entityData, ISaveable[] saveables)
        {
            foreach (ISaveable saveable in saveables)
            {
                ISaveSerializer serializer = saveable.Serializer;
                _container.Inject(serializer);
                if (entityData.Data.TryGetValue(serializer.Key, out JToken token))
                {
                    serializer.Deserialize(token);
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