using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(TouchHitComponent))]
    public class Trap : MonoBehaviour
    {
        private HealthComponent _health;
        private DamageComponent _damage;
        
        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _damage = GetComponent<DamageComponent>();

            _damage.OnHit += () =>
            {
                _health.SetZero();
            };

            _health.OnDied += () =>
            {
                StartCoroutine(DestroyNextFixedUpdate());
            };
        }

        IEnumerator DestroyNextFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            Destroy(gameObject);
        }
    }
}