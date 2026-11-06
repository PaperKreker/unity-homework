using System;
using Game;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(InputComponent))]
    public class Character : MonoBehaviour
    {
        [SerializeField]
        private JumpComponent _jump;
        
        [FormerlySerializedAs("_pushAttack")] [SerializeField]
        private DirectPushComponent directPush;
        
        [FormerlySerializedAs("_tossAttack")] [SerializeField]
        private DirectPushComponent toss;
        
        private MoveTransformComponent _moveTransform;
        private HealthComponent _health;
        private InputComponent _input;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _health = GetComponent<HealthComponent>();
            _input = GetComponent<InputComponent>();

            _health.OnDied += () =>
            {
                GetComponent<Rigidbody2D>().simulated = false;
            };
        }

        public void Move(float direction)
        {
            if (_health.IsDied) 
                return;

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

            directPush.AttackDelayed();
        }

        public void Toss()
        {
            if (_health.IsDied) 
                return;

            toss.AttackDelayed();
        }
    }
}