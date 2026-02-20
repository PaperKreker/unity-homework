using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class EnemyMovement : ShipMovement<EnemyShip>
    {
        private Vector2 _destination;

        protected override void OnEnable()
        {
            base.OnEnable();
            _shipController.OnRespawn += ChangeDestination;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _shipController.OnRespawn -= ChangeDestination;
        }

        private void ChangeDestination(EnemyShip.RespawnArgs respawnArgs)
        {
            _destination = respawnArgs.destination;
        }

        public override void Move()
        {
            Vector2 distance = _destination - (Vector2)transform.position;
            MoveStep(distance.normalized);
        }

        public void Stop()
        {
            MoveStep(Vector2.zero);
        }
    }
}