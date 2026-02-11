using UnityEngine;

namespace Game
{
    // +
    public sealed class EnemyShip : ShipController
    {
        [Header("Enemy")]
        public ShipController Target { get; private set; }

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private IEnemyDespawner _despawner;
        private Vector2 _destination;

        public void SetDespawner(IEnemyDespawner despawner) => _despawner = despawner;

        private void OnEnable() => this.OnDead += this.OnCharacterDead;

        private void OnDisable() => this.OnDead -= this.OnCharacterDead;

        private void OnCharacterDead() => _despawner.Despawn(this);

        public void Respawn(ShipController target, Vector3 spawnPosition, Vector2 destination)
        {
            transform.position = spawnPosition;
            _destination = destination;
            Target = target;
            ResetHealth();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (this.CurrentHealth <= 0 || this.Target == null || this.Target.CurrentHealth <= 0)
                return;

            if (!TryMove())
            {
                this.Fire();
            }
        }

        private bool TryMove()
        {
            Vector2 distance = _destination - (Vector2)this.transform.position;
            bool isNotReached = distance.sqrMagnitude > _stoppingDistance * _stoppingDistance;

            MoveDirection = isNotReached ? distance.normalized : Vector3.zero;

            if (isNotReached)
            {
                _motor.MoveStep(distance.normalized);
            }
            return isNotReached;
        }
    }
}