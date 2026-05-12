using UnityEngine;

namespace Game
{
    public class RaycastComponent : MonoBehaviour
    {
        [SerializeField]
        private float _distance = 1.0f;

        [SerializeField] 
        private LayerMask _layerMask;
        
        public Rigidbody2D Cast(Vector3 startPoint, Vector3 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, _distance, _layerMask);
            return hit.rigidbody;
        }
    }
}