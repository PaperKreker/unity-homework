using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;
        public event Action OnRespawn;
        public event Action OnHit;

        public TeamType Team { get => _config.Team; }

        private ShipConfig _config;
        private Vector2 _direction;

        private void OnTriggerEnter2D(Collider2D other)
        {
            TryHit(other);
        }

        public void Respawn(Vector2 direction, Vector2 position, ShipConfig config)
        {
            _direction = direction;
            _config = config;

            transform.position = position;
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

        public void Move()
        {
            Vector3 moveStep = _direction * _config.BulletSpeed * Time.fixedDeltaTime;
            transform.position += moveStep;
        }

        private bool TryHit(Collider2D other)
        {
            if (!other.TryGetComponent(out Ship ship))
                return false;

            if (Team != TeamType.None && Team != ship.Team)
            {
                // Deal damage to target:
                if (_config.BulletDamage > 0)
                {
                    ship.Hit(_config.BulletDamage);
                }

                OnHit?.Invoke();
                OnDestroy?.Invoke(this);
                return true;
            }

            return false;
        }
    }
}