using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyBehaviour : MonoBehaviour
    {
        public event Action<EnemyBehaviour> OnDead;

        [SerializeField]
        private Ship _ship;

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private Vector2 _destination;
        private State _state;
        private Ship _target;


        private void Start()
        {
            ResetState();
        }

        private void OnEnable()
        {
            _ship.OnDead += Dead;
        }

        private void OnDisable()
        {
            _ship.OnDead -= Dead;
        }

        public void Respawn(Ship target, Vector3 spawnPosition, Vector2 destination)
        {
            transform.position = spawnPosition;
            _destination = destination;
            _target = target;
            _ship.ResetHealth();
            ResetState();
        }

        private void FixedUpdate()
        {
            if (_target != null && _target.IsAlive())
            {
                switch (_state)
                {
                    case State.Moving:
                        Move();
                        break;
                    case State.Firing:
                        Fire();
                        break;
                }
            }
        }

        public void ChangeState(State newState)
        {
            _state = newState;
        }

        private void ResetState()
        {
            _state = State.Moving;
        }

        private void Dead()
        {
            OnDead?.Invoke(this);
        }

        private bool IsReached()
        {
            Vector2 distance = _destination - (Vector2)transform.position;
            return distance.sqrMagnitude <= _stoppingDistance * _stoppingDistance;
        }

        private void Move()
        {
            Vector2 direction = (_destination - (Vector2)transform.position).normalized;
            _ship.Move(direction);

            if (IsReached())
            {
                Stop();
                ChangeState(State.Firing);
            }
        }
        private void Stop()
        {
            _ship.Move(Vector2.zero);
        }

        private void Fire()
        {
            _ship.FireTarget(_target.transform);
        }

        public enum State
        {
            Moving,
            Firing,
        }
    }
}