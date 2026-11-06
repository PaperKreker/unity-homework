using UnityEngine;

namespace Game
{
    public class PushComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _force;
        
        public void Push(Rigidbody2D target)
        {
            Push(target, Vector2.one);
        }
        
        public void Push(Rigidbody2D target, Vector2 direction)
        {
            if (!target)
                return;

            Vector2 directionForce = 
                transform.right * (_force.x * direction.x) + 
                transform.up * (_force.y * direction.y);
            target.AddForce(directionForce, ForceMode2D.Impulse);
        }
    }
}
