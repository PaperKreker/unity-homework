using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletController : MonoBehaviour
    {
        public event Action<BulletController> OnDestroy;
        public event Action OnRespawn;
        public event Action OnHit;

        public TeamType Team { get; private set; }

        private Vector2 _direction;
        private float _speed;
        private int _damage;

        private void FixedUpdate()
        {
            Move();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            TryHit(other);
        }

        public void Respawn(RespawnArgs respawnArgs)
        {
            _direction = respawnArgs.Direction;
            _damage = respawnArgs.Damage;
            _speed = respawnArgs.Speed;
            Team = respawnArgs.Team;

            transform.position = respawnArgs.Position;
            transform.rotation = Quaternion.LookRotation(_direction, Vector3.forward);
            gameObject.layer = Team switch
            {
                TeamType.None   => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy  => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(Team), Team, null)
            };

            OnRespawn?.Invoke();
        }

        private void Move()
        {
            Vector3 moveStep = _direction * _speed * Time.fixedDeltaTime;
            transform.position += moveStep;
        }

        private bool TryHit(Collider2D other)
        {
            if (!other.TryGetComponent(out ShipController ship))
                return false;

            if (Team != TeamType.None && Team != ship.Team)
            {
                // Deal damage to target:
                if (_damage > 0)
                {
                    ship.Hit(_damage);
                }

                OnHit?.Invoke();
                OnDestroy?.Invoke(this);
                return true;
            }

            return false;
        }

        public struct RespawnArgs
        {
            public Vector2 Direction;
            public Vector2 Position; 
            public TeamType Team;
            public float Speed;
            public int Damage;
        }
    }
}