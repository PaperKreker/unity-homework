using UnityEngine;

namespace Game
{
    public abstract class ShipMovement<T_ShipController> : ShipMovementBase where T_ShipController : ShipController
    {
        [SerializeField]
        protected T_ShipController _shipController;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        protected virtual void OnEnable()
        {
            _shipController.OnMove += Move;
        }

        protected virtual void OnDisable()
        {
            _shipController.OnMove -= Move;
        }

        protected void MoveStep(Vector2 direction)
        {
            float speed = _shipController.Config.MoveSpeed * Time.fixedDeltaTime;
            Vector2 newPosition = _rigidbody.position + direction * speed;
            _rigidbody.MovePosition(newPosition);
            Direction = direction;
        }
    }
}