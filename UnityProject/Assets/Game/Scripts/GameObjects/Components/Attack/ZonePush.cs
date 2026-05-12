using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ZonePush : MonoBehaviour
    {
        [SerializeField] 
        private TriggerComponent _triggerComponent;
        
        [SerializeField] 
        private PushComponent _push;

        private List<Rigidbody2D> _targets = new();

        private void OnEnable()
        {
            _triggerComponent.OnEntered += AddTarget;
            _triggerComponent.OnExited += RemoveTarget;
        }

        private void OnDisable()
        {
            _triggerComponent.OnEntered -= AddTarget;
            _triggerComponent.OnExited -= RemoveTarget;
        }

        public void Push()
        {
            foreach (Rigidbody2D target in _targets)
            {
                _push.Push(target, GetDirection(target.transform));
            }
        }

        private void AddTarget(Collider2D col)
        {
            if (col.attachedRigidbody != null && !_targets.Contains(col.attachedRigidbody))
            {
                _targets.Add(col.attachedRigidbody);
            }
        }
        private void RemoveTarget(Collider2D col)
        {
            _targets.Remove(col.attachedRigidbody);
        }

        private Vector2 GetDirection(Transform target)
        {
            return new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1.0f);
        }
    }
}