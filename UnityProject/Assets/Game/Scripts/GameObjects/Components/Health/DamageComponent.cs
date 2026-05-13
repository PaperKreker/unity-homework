using System;
using UnityEngine;

namespace Game
{
    public class DamageComponent : MonoBehaviour
    {
        public event Action OnHit;
        
        [SerializeField]
        private float _damage;

        public void Hit(GameObject target)
        {
            HealthComponent health = target.transform.GetComponentInParent<HealthComponent>();
            if (health != null)
            {
                health.TakeDamage(_damage);
                OnHit?.Invoke();
            }
        }
    }
}
