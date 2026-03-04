using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class ShipMovement
    {
        public Vector2 Direction { get; private set; }

        [SerializeField]
        private Rigidbody2D _rigidbody;

        private ShipConfig _config;

        public void Initialize(ShipConfig config)
        {
            _config = config;
        }

        public void MoveStep(Vector2 direction)
        {
            float speed = _config.MoveSpeed * Time.fixedDeltaTime;
            Vector2 newPosition = _rigidbody.position + direction * speed;
            _rigidbody.MovePosition(newPosition);
            Direction = direction;
        }
    }
}