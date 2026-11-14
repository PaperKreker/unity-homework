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
        private TouchHitComponent _touchHit;
        
        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _touchHit = GetComponent<TouchHitComponent>();
        }
        
        private void OnEnable()
        {
            _touchHit.OnHit += _health.SetZero;
            _health.OnDied += Die;
        }
        
        private void OnDisable()
        {
            _touchHit.OnHit -= _health.SetZero;
            _health.OnDied -= Die;
        }

        IEnumerator DestroyNextFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            Destroy(gameObject);
        }
        
        private void Die()
        {
            StartCoroutine(DestroyNextFixedUpdate());
        }
    }
}