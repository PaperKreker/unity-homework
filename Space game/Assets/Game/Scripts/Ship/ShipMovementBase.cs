using UnityEngine;


namespace Game
{
    public abstract class ShipMovementBase : MonoBehaviour
    {
        public Vector2 Direction { get; protected set; }

        public abstract void Move();
    }
}
