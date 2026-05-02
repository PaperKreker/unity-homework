using System.Collections.Generic;
using Modules.Entities;
using SampleGame.Common;
using SampleGame.SaveSystem;
using Zenject;

namespace SampleGame.Gameplay.Serializers
{
    public struct TargetObjectSerializer : ISaveSerializer<TargetObjectSerializer.Snapshot>
    {
        public string Key => "TargetObject";
        
        [Inject]
        public EntityWorld World;
        
        private readonly TargetObject _targetObject;

        public TargetObjectSerializer(TargetObject targetObject)
        {
            World = null;
            _targetObject = targetObject;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_targetObject);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(World, _targetObject);
        }
        
        public struct Snapshot
        {
            public int TargetId;

            public Snapshot(TargetObject targetObject)
            {
                if (targetObject.Value != null)
                {
                    TargetId = targetObject.Value.Id;
                }
                else
                {
                    TargetId = -1;
                }
            }

            public void Restore(EntityWorld entityWorld, TargetObject targetObject)
            {
                if (entityWorld.Has(TargetId))
                {
                    targetObject.Value = entityWorld.Get(TargetId);
                }
            }
        }
    }
}