using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyShip : ShipController
    {
        public override TeamType Team { get => TeamType.Enemy; }
        public ShipController Target { get; private set; }

        public event Action<RespawnArgs> OnRespawn;
        public event Action<EnemyShip> OnDead;

        [Header("Enemy")]
        private Dictionary<State, Action> _stateActions = new Dictionary<State, Action>();
        private State _state;


        private void Start()
        {
            _stateActions.Add(State.Moving, Move);
            _stateActions.Add(State.Firing, TryFire);

            ResetState();
        }

        private void OnEnable()
        {
            Health.OnDead += Dead;
        }

        private void OnDisable()
        {
            Health.OnDead -= Dead;
        }

        public void Respawn(RespawnArgs respawnArgs)
        {
            transform.position = respawnArgs.spawnPosition;
            Target = respawnArgs.target;
            ResetState();
            Health.ResetHealth();
            OnRespawn?.Invoke(respawnArgs);
        }

        private void FixedUpdate()
        {
            if (Health.IsAlive() && Target != null && Target.Health.IsAlive())
            {
                _stateActions[_state]();
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

        public struct RespawnArgs
        {
            public ShipController target;
            public Vector3 spawnPosition;
            public Vector2 destination;
        }
        public enum State
        {
            Moving,
            Firing,
        }
    }
}