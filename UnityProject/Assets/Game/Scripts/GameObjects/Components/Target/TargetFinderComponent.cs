using UnityEngine;

namespace Game
{
    public class TargetFinderComponent : MonoBehaviour
    {
        public GameObject Target => _target;
        
        [SerializeField] 
        private TriggerComponent _trigger;
        
        [SerializeField] 
        private LayerMask _layerMask;

        private GameObject _target;
        
        private void OnEnable()
        {
            _trigger.OnEntered += SelectTarget;
        }
        
        private void OnDisable()
        {
            _trigger.OnEntered -= SelectTarget;
        }
        
        private void SelectTarget(Collider2D otherCollider)
        {
            if ((_layerMask.value & (1 << otherCollider.gameObject.layer)) == 0)
                return;

            _target = otherCollider.gameObject;
        }
    }
}