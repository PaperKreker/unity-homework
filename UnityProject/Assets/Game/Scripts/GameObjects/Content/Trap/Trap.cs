using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(HitComponent))]
    public class Trap : MonoBehaviour
    {
        private HealthComponent _health;
        private HitComponent _hit;
        
        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _hit = GetComponent<HitComponent>();

            _hit.OnHit += () =>
            {
                _health.SetZero();
            };

            _health.OnDied += () =>
            {
                Destroy(gameObject);
            };
        }
    }
}