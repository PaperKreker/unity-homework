using UnityEngine;

namespace Game
{
    public class PlayerMovement : ShipMovement<PlayerShip>
    {
        public override void Move()
        {
            float dx = Input.GetAxisRaw("Horizontal");
            float dy = Input.GetAxisRaw("Vertical");
            MoveStep(new Vector2(dx, dy));
        }
    }
}