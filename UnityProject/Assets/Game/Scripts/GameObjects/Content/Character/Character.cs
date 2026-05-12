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
        private RequestComponent _jumpRequest;
        
        [SerializeField]
        private RequestComponent _pushRequest;
        
        [SerializeField]
        private RequestComponent _tossRequest;
        
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

        void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            _moveTransform.Move(new Vector2(Mathf.Abs(_input.HorizontalAxis), 0.0f));
        }

        private void Update()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            if (_input.JumpInput)
            {
                _jumpRequest.Request();
            }

            if (_input.PushInput)
            {
                _pushRequest.Request();
            }
            
            if (_input.TossInput)
            {
                _tossRequest.Request();
            }
        }
    }
}