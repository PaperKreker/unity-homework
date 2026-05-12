using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField] 
        private Character _character;
        
        [SerializeField] 
        private RequestComponent _jumpRequest;
        
        [SerializeField]
        private AudioComponent _audio;

        private TakeDamageColorComponent _takeDamageColor;
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private InputComponent _input;
        private LookComponent _look;
        private float lastMoveDirection;
        
        private readonly int _jumpKey = Animator.StringToHash("Jump");
        private readonly int _isMovingKey = Animator.StringToHash("IsMoving");
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");
        private readonly int _deathKey = Animator.StringToHash("Death");

        private void Awake()
        {
            _grounded = _character.GetComponent<GroundedComponent>();
            _health = _character.GetComponent<HealthComponent>();
            _input = _character.GetComponent<InputComponent>();
            _look = _character.GetComponent<LookComponent>();
            
            _takeDamageColor = GetComponent<TakeDamageColorComponent>();
            
            _jumpRequest.OnInvoke += () =>
            {
                _animator.SetTrigger(_jumpKey);
                _audio.Play("Jump");
            };

            _health.OnDied += () =>
            {
                _animator.SetTrigger(_deathKey);
                _audio.Play("Death");
            };

            _health.OnHealthChanged += (_) =>
            {
                _takeDamageColor.TakeDamage();
                _audio.Play("TakeDamage");
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
            _animator.SetBool(_isMovingKey, Mathf.Abs(_input.HorizontalAxis) > 0);
            _animator.SetBool(_isGroundedKey, _grounded.IsGrounded);
        }
        
        private void RefreshLook()
        {
            if (!_input.HorizontalAxis.Equals(0.0f))
            {
                lastMoveDirection = _input.HorizontalAxis;
            }
            _look.Look(lastMoveDirection);
        }
    }
}