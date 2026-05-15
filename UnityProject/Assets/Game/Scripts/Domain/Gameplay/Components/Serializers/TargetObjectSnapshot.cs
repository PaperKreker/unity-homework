using Modules.Entities;
using SampleGame.SaveSystem;

namespace SampleGame.Gameplay.Serializers
{
    [System.Serializable]
    public class TargetObjectSnapshot : ISnapshot<TargetObject>
    {
        public int TargetId;
        private EntityWorld _entityWorld;

        public TargetObjectSnapshot(EntityWorld entityWorld)
        {
            _entityWorld = entityWorld;
        }
        
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

        public void Restore(TargetObject targetObject)
        {
            if (_entityWorld.Has(TargetId))
            {
                targetObject.Value = _entityWorld.Get(TargetId);
            }
        }
    }
}