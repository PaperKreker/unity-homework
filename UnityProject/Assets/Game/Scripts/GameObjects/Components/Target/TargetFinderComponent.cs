using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class TargetFinderComponent : MonoBehaviour
    {
        public Rigidbody2D Target => _targets.Count > 0 ? _targets[0] : null;
        public IReadOnlyList<Rigidbody2D> Targets => _targets;
        
        [SerializeField] 
        private Rigidbody2D _ignoreBody;
        
        [SerializeField] 
        private ContactFilter2D _contactFilter;
        
        [SerializeField] 
        private float _radius;
        
        private readonly List<Rigidbody2D> _targets = new();

        private void FixedUpdate()
        {
            SelectTargets();
        }

        private void SelectTargets()
        {
            List<Collider2D> colliders = new ();
            Physics2D.OverlapCircle(transform.position, _radius, _contactFilter, colliders);
            
            _targets.Clear();
            for (int i = 0; i < colliders.Count; ++i)
            {
                if (colliders[i].attachedRigidbody != null && 
                    colliders[i].attachedRigidbody != _ignoreBody &&
                    !_targets.Contains(colliders[i].attachedRigidbody))
                {
                    _targets.Add(colliders[i].attachedRigidbody);
                }
            }
        }
    }
}