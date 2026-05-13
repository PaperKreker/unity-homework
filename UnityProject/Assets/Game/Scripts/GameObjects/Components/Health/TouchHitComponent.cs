using System;
using UnityEngine;

namespace Game
{
    public class TouchHitComponent : MonoBehaviour
    {
        [SerializeField]
        private DamageComponent _damage;
        
        [SerializeField]
        private CollisionComponent _collision;
        
        [SerializeField]
        private LayerMask _layerMask;

        private void OnEnable() => _collision.OnEntered += OnCollisionEntered;

        private void OnDisable() => _collision.OnEntered -= OnCollisionEntered;

        private void OnCollisionEntered(Collision2D col)
        {
            if ((_layerMask.value & (1 << col.gameObject.layer)) == 0)
                 return;

            _damage.Hit(col.gameObject);
        }
    }
}
