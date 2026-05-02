using System.Collections.Generic;
using Modules.Entities;
using SampleGame.Common;
using SampleGame.SaveSystem;
using Zenject;

namespace SampleGame.Gameplay.Serializers
{
    public readonly struct ResourceBagSerializer : ISaveSerializer<ResourceBagSerializer.Snapshot>
    {
        public string Key => "ResourceBag";
        
        private readonly ResourceBag _resourceBag;

        public ResourceBagSerializer(ResourceBag resourceBag)
        {
            _resourceBag = resourceBag;
        }
        
        public Snapshot Serialize()
        {
            return new Snapshot(_resourceBag);
        }

        public void Deserialize(Snapshot snapshot)
        {
            snapshot.Restore(_resourceBag);
        }
        
        public struct Snapshot
        {
            public ResourceType Type;
            public int Current;

            public Snapshot(ResourceBag resourceBag)
            {
                Type = resourceBag.Type;
                Current = resourceBag.Current;
            }

            public void Restore(ResourceBag resourceBag)
            {
                resourceBag.Type = Type;
                resourceBag.Current = Current;
            }
        }
    }
}