using Modules.UI;
using Modules.Utils;
using UnityEngine;

namespace Game
{
    // +
    public sealed class PlayerShip : ShipController
    {
        [SerializeField]
        private TransformBounds _playerArea;

        public void Update()
        {
            ReadFireInput();
            ReadMoveInput();

            if (this.CurrentHealth > 0)
            {
                _motor.MoveStep(this.MoveDirection);
            }
        }

        private void LateUpdate()
        {
            ClampPosition();
        }

        private void ReadFireInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                this.Fire();
        }

        private void ReadMoveInput()
        {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            this.MoveDirection = new Vector2(dx, dy);
        }

        private void ClampPosition()
        {
            this.transform.position = _playerArea.ClampInBounds(this.transform.position);
        }
    }
}