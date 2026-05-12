using System;
using UnityEngine;

namespace Game
{
    public class HitComponent : MonoBehaviour
    {
        public event Action OnHit;
        
        [SerializeField]
        private CollisionComponent _collision;
        
        [SerializeField]
        private LayerMask _layerMask;
        
        [SerializeField]
        private float _damage;

        private void OnEnable() => _collision.OnEntered += OnCollisionEntered;

        private void OnDisable() => _collision.OnEntered -= OnCollisionEntered;

        private void OnCollisionEntered(Collision2D col)
        {
            if ((_layerMask.value & (1 << col.gameObject.layer)) == 0)
                 return;
            
            HealthComponent health = col.transform.GetComponentInParent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(_damage);
                OnHit?.Invoke();
            }
        }
    }
}
