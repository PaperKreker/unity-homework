using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(HealthComponent))]
    public class Character : MonoBehaviour
    {
        public float MoveInput { get; private set; }

        [SerializeField]
        private JumpComponent _jump;
        
        [SerializeField]
        private DirectPushComponent _push;
        
        [SerializeField]
        private DirectPushComponent _toss;
        
        private MoveTransformComponent _moveTransform;
        private HealthComponent _health;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _health = GetComponent<HealthComponent>();
            
            _push.SetCondition(() => _health.IsAlive);
            _toss.SetCondition(() => _health.IsAlive);
        }

        private void OnEnable()
        {
            _health.OnDied += Die;
        }
        
        private void OnDisable()
        {
            _health.OnDied -= Die;
        }

        public void Move(float direction)
        {
            if (_health.IsDied) 
                return;

            MoveInput = direction;
            _moveTransform.Move(new Vector2(Mathf.Abs(direction), 0.0f));
        }

        public void Jump()
        {
            if (_health.IsDied) 
                return;
            
            _jump.JumpDelayed();
        }

        public void Push()
        {
            if (_health.IsDied) 
                return;

            _push.AttackDelayed();
        }

        public void Toss()
        {
            if (_health.IsDied) 
                return;

            _toss.AttackDelayed();
        }

        private void Die()
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}