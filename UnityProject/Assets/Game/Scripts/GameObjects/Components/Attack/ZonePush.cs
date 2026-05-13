using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ZonePush : MonoBehaviour
    {
        [SerializeField] 
        private TargetFinderComponent _targetFinder;
        
        [SerializeField] 
        private PushComponent _push;

        public void Push()
        {
            foreach (Rigidbody2D target in _targetFinder.Targets)
            {
                _push.Push(target, GetDirection(target.transform));
            }
        }

        private Vector2 GetDirection(Transform target)
        {
            return new Vector2(Mathf.Sign(target.position.x - transform.position.x), 1.0f);
        }
    }
}