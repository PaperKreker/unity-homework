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
            EntityData[] entityDatas = new EntityData[entities.Count];

            int i = 0;
            foreach (Entity entity in entities)
            {
                entityDatas[i] = GetEntityData(entity);
                ++i;
            }

            return new EntityContainer()
            {
                Entities = entityDatas,
            };
        }

        private EntityData GetEntityData(Entity entity)
        {
            ISaveable[] saveables = entity.GetComponents<ISaveable>();
            
            return new EntityData()
            {
                Name = entity.Name,
                Position = entity.transform.position,
                Rotation = entity.transform.rotation,
                Data = GetSerializeData(saveables),
            };
        }

        private JObject GetSerializeData(ISaveable[] saveables)
        {
            JObject data = new ();
            for (int j = 0; j < saveables.Length; ++j)
            {
                ISaveSerializer serializer = saveables[j].Serializer;
                data.Add(serializer.Key, serializer.Serialize());
            }

            return data;
        }

        public void Deserialize(EntityContainer entityContainer)
        {
            _entityWorld.DestroyAll();
            Dictionary<EntityData, Entity> spawnedEntities = new();
            foreach (EntityData entityData in entityContainer.Entities)
            {
                if (!_entityCatalog.FindConfig(entityData.Name, out EntityConfig entityConfig))
                    continue;
                
                Entity entity = _entityWorld.Spawn(entityConfig.Name, entityData.Position, entityData.Rotation);
                spawnedEntities.Add(entityData, entity);
            }
            
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
            public SerializableVector3 Position;
            public SerializableQuaternion Rotation;
            public JObject Data;
        }
    }
}