using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PushComponent))]
    public class DirectPushComponent : MonoBehaviour
    {
        public event Action OnAttack; 
        
        [SerializeField] 
        private Transform _firePoint;
        
        [SerializeField] 
        private CooldownComponent _cooldown;
        
        [SerializeField] 
        private DelayComponent _delay;
        
        [SerializeField] 
        private RaycastComponent _raycast;
        
        private PushComponent _push;

        private void Awake()
        {
            _push = GetComponent<PushComponent>();
        }

        private void OnEnable()
        {
            _delay.OnExpire += TryAttack;
        }
        
        private void OnDisable()
        {
            _delay.OnExpire -= TryAttack;
        }

        public void AttackDelayed()
        {
            StartCoroutine(_delay.StartDelay());
        }
        
        private void TryAttack()
        {
            if (_cooldown.IsExpired)
            {
                Attack();
            }
        }
        
        private void Attack()
        {
            _cooldown.Reset();
            _push.Push(GetTarget());
            
            OnAttack?.Invoke();
        }

        private Rigidbody2D GetTarget()
        {
            return _raycast.Cast(_firePoint.position, _firePoint.right);
        }
    }
}