using Modules.UI;
using Modules.Utils;
using System;
using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerShip : ShipController
    {
        public override TeamType Team { get => TeamType.Player; }

        [SerializeField]
        private TransformBounds _playerArea;


        public void Update()
        {
            ReadFireInput();

            if (Health.IsAlive())
            {
                Move();
            }
        }

        private void LateUpdate()
        {
            ClampPosition();
        }

        private void ReadFireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                TryFire();
        }

        private void ClampPosition()
        {
            transform.position = _playerArea.ClampInBounds(transform.position);
        }
    }
}