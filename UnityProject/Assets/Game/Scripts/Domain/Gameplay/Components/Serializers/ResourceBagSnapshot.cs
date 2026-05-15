using SampleGame.Common;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class ResourceBagSnapshot : ISnapshot<ResourceBag>
    {
        public ResourceType Type;
        public int Current;

        public void Save(ResourceBag resourceBag)
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