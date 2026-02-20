using UnityEngine;

namespace Game
{
    public class EnemyStateSwitch : MonoBehaviour
    {
        [SerializeField] 
        private EnemyShip _ship;

        [SerializeField]
        private EnemyMovement _movement;

        [SerializeField]
        private float _stoppingDistance = 0.25f;

        private Vector2 _destination;

        private void OnEnable()
        {
            _ship.OnRespawn += ChangeDestination;
            _ship.OnMove += CheckIfReached;
        }

        private void OnDisable()
        {
            _ship.OnRespawn -= ChangeDestination;
            _ship.OnMove -= CheckIfReached;
        }

        private void ChangeDestination(EnemyShip.RespawnArgs respawnArgs)
        {
            _destination = respawnArgs.destination;
        }

        private void CheckIfReached()
        {
            Vector2 distance = _destination - (Vector2)transform.position;
            if (distance.sqrMagnitude <= _stoppingDistance * _stoppingDistance)
            {
                _ship.ChangeState(EnemyShip.State.Firing);
                _movement.Stop();
            }
        }
    }
}