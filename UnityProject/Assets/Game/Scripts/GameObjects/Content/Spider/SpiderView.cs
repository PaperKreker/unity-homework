using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [RequireComponent(typeof(TakeDamageColorComponent))]
    [RequireComponent(typeof(LookComponent))]
    public class SpiderView : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField] 
        private Spider _spider;

        private TakeDamageColorComponent _takeDamageColor;
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private PatrolComponent _patrol;
        private LookComponent _look;
        private float lastMoveDirection;
        
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");
        private readonly int _isMovingKey = Animator.StringToHash("IsMoving");
        private readonly int _deathKey = Animator.StringToHash("Death");

        private void Awake()
        {
            _grounded = _spider.GetComponent<GroundedComponent>();
            _health = _spider.GetComponent<HealthComponent>();
            _patrol = _spider.GetComponent<PatrolComponent>();
            
            _takeDamageColor = GetComponent<TakeDamageColorComponent>();
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
            float direction = _patrol.GetDirection().x;
            _animator.SetBool(_isMovingKey, Mathf.Abs(direction) > 0);
            _animator.SetBool(_isGroundedKey, _grounded.IsGrounded);
        }
        
        private void RefreshLook()
        {
            float direction = _patrol.GetDirection().x;
            if (!direction.Equals(0.0f))
            {
                lastMoveDirection = direction;
            }
            _look.Look(-lastMoveDirection);
        }
    }
}