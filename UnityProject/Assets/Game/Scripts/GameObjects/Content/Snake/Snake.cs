using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(TargetFinderComponent))]
    [RequireComponent(typeof(TouchHitComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(PushComponent))]
    
    public class Snake : MonoBehaviour
    {
        public bool IsMoving => _targetFinder.Target != null;
        
        private MoveTransformComponent _moveTransform;
        private TargetFinderComponent _targetFinder;
        private HealthComponent _health;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _targetFinder = GetComponent<TargetFinderComponent>();
            _health = GetComponent<HealthComponent>();
        }
        
        private void OnEnable()
        {
            _health.OnDied += Die;
        }
        
        private void OnDisable()
        {
            _health.OnDied -= Die;
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
        
        private void Die()
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
