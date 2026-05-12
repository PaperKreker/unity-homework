using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [RequireComponent(typeof(TakeDamageColorComponent))]
    [RequireComponent(typeof(TargetFinderComponent))]
    [RequireComponent(typeof(LookComponent))]
    public class MonkeyView : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField] 
        private Monkey _monkey;

        private TakeDamageColorComponent _takeDamageColor;
        private TargetFinderComponent _targetFinder;
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private LookComponent _look;
        
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");
        private readonly int _deathKey = Animator.StringToHash("Death");

        private void Awake()
        {
            _grounded = _monkey.GetComponent<GroundedComponent>();
            _health = _monkey.GetComponent<HealthComponent>();
            
            _takeDamageColor = GetComponent<TakeDamageColorComponent>();
            _targetFinder = GetComponent<TargetFinderComponent>();
            _look = GetComponent<LookComponent>();

            _health.OnDied += () =>
            {
                _animator.SetTrigger(_deathKey);
            };

            _health.OnHealthChanged += (_) =>
            {
                _takeDamageColor.TakeDamage();
            };
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            RefreshAnimator();
            RefreshLook();
        }

        private void RefreshAnimator()
        {
            _animator.SetBool(_isGroundedKey, _grounded.IsGrounded);
        }
        
        private void RefreshLook()
        {
            if (_targetFinder.Target != null)
            {
                _look.Look(_targetFinder.Target.transform);
            }
        }

        
    }
}