using SampleGame.Common;

namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class ResourceBagSnapshot
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