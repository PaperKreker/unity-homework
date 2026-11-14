using System;
using UnityEngine;

namespace Game
{
    public class TouchHitComponent : MonoBehaviour
    {
        public event Action OnHit;
        
        [SerializeField]
        private float _damage;
        
        [SerializeField]
        private PushComponent _push;
        
        [SerializeField]
        private CollisionComponent _collision;
        
        [SerializeField]
        private LayerMask _layerMask;

        private void OnEnable() => _collision.OnEntered += OnCollisionEntered;

        private void OnDisable() => _collision.OnEntered -= OnCollisionEntered;

        private void OnCollisionEntered(Collision2D col)
        {
            if ((_layerMask.value & (1 << col.gameObject.layer)) == 0)
                 return;
            
            Hit(col);
        }
        
        private void Hit(Collision2D target)
        {
            bool wasHit = false;
            wasHit |= TryDamage(target.transform);
            wasHit |= TryPush(target.rigidbody);

            if (wasHit)
            {
                OnHit?.Invoke();
            }
        }

        private bool TryDamage(Transform target)
        {
            HealthComponent health = target.GetComponentInParent<HealthComponent>();
            if (health)
            {
                health.TakeDamage(_damage);
                return true;
            }

            return false;
        }
        
        private bool TryPush(Rigidbody2D target)
        {
            if (_push)
            {
                Vector2 direction = new (
                    Mathf.Sign(target.transform.position.x - transform.position.x), 
                    0.0f);
                
                _push.Push(target, direction);
                return true;
            }

            return false;
        }
    }
}
