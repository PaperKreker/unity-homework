using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GroundedComponent))]
    public class JumpComponent : MonoBehaviour
    {
        public event Action OnJump;
        
        [SerializeField] 
        private CooldownComponent _cooldown;
        [SerializeField]
        private DelayComponent _delay;
        [SerializeField] 
        private Vector2 _force = new (0.0f, 10.0f);
        [SerializeField]
        private Rigidbody2D _rigidbody;
        
        private GroundedComponent _grounded;

        private void Awake()
        {
            _grounded = GetComponent<GroundedComponent>();
        }

        private void OnEnable()
        {
            _delay.OnExpire += TryJump;
        }
        
        private void OnDisable()
        {
            _delay.OnExpire += TryJump;
        }
        
        public void JumpDelayed()
        {
            StartCoroutine(_delay.StartDelay());
        }

        private void TryJump()
        {
            if (_cooldown.IsExpired && _grounded.IsGrounded)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _cooldown.Reset();
            _rigidbody.AddForce(_force, ForceMode2D.Impulse);
            
            OnJump?.Invoke();
        }
    }
}
