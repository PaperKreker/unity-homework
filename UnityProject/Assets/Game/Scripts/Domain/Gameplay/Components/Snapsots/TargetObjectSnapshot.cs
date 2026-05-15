using Modules.Entities;

namespace SampleGame.Gameplay.Snapshots
{
    [System.Serializable]
    public class TargetObjectSnapshot
    {
        public int TargetId;
        
        public void Save(TargetObject targetObject)
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

        public void Restore(TargetObject targetObject, EntityWorld entityWorld)
        {
            if (entityWorld.Has(TargetId))
            {
                targetObject.Value = entityWorld.Get(TargetId);
            }
        }
    }
}