using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(TargetFinderComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageComponent))]
    [RequireComponent(typeof(PushComponent))]
    
    public class Snake : MonoBehaviour
    {
        public bool IsMoving => _targetFinder.Target != null;
        
        private MoveTransformComponent _moveTransform;
        private TargetFinderComponent _targetFinder;
        private DamageComponent _damage;
        private HealthComponent _health;
        private PushComponent _push;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _targetFinder = GetComponent<TargetFinderComponent>();
            _damage = GetComponent<DamageComponent>();
            _health = GetComponent<HealthComponent>();
            _push = GetComponent<PushComponent>();

            _health.OnDied += () =>
            {
                GetComponent<Rigidbody2D>().simulated = false;
            };
            
            
            _damage.OnHit += () =>
            {
                _push.Push(_targetFinder.Target);
            };
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            if (_targetFinder.Target != null)
            {
                _moveTransform.Move(new Vector2(1.0f, 0.0f));
            }
        }
    }
}
