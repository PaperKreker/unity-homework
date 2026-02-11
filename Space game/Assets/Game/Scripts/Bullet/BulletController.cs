using Modules.Utils;
using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class BulletController : MonoBehaviour
    {
        public event Action OnRespawn;
        public event Action OnHit;
        public event Action<BulletController> OnDestroy;

        public TeamType Team { get; private set; }

        private TransformBounds _levelBounds;
        private Vector2 _direction;
        private float _speed;
        private int _damage;

        private void FixedUpdate()
        {
            Move();
            CheckBounds();
        }

        private void Move()
        {
            Vector3 moveStep = _direction * _speed * Time.fixedDeltaTime;
            transform.position += moveStep;
        }

        private void CheckBounds()
        {
            if (!_levelBounds.InBounds(transform.position))
            {
                OnDestroy?.Invoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out ShipController ship))
                return;

            if (Team == TeamType.Player && ship is EnemyShip ||
                Team == TeamType.Enemy && ship is PlayerShip)
            {
                // Deal damage to target:
                if (_damage > 0)
                {
                    ship.Hit(_damage);
                }

                OnHit?.Invoke();
                OnDestroy?.Invoke(this);
            }
        }

        public void Respawn(TransformBounds levelBounds, Vector2 position, TeamType team, Vector2 direction, float speed, int damage)
        {
            _levelBounds = levelBounds;
            _direction = direction;
            _damage = damage;
            _speed = speed;
            Team = team;

            transform.position = position;
            transform.rotation = Quaternion.LookRotation(_direction, Vector3.forward);
            gameObject.layer = Team switch
            {
                TeamType.None => LayerMask.NameToLayer("Default"),
                TeamType.Player => LayerMask.NameToLayer("PlayerBullet"),
                TeamType.Enemy => LayerMask.NameToLayer("EnemyBullet"),
                _ => throw new ArgumentOutOfRangeException(nameof(Team), Team, null)
            };

            OnRespawn?.Invoke();
        }
    }
}