using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(PatrolComponent))]
    public class Spider : MonoBehaviour
    {
        private MoveTransformComponent _moveTransform;
        private HealthComponent _health;
        private PatrolComponent _patrol;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _health = GetComponent<HealthComponent>();
            _patrol = GetComponent<PatrolComponent>();
            
            _health.OnDied += () =>
            {
                GetComponent<Rigidbody2D>().simulated = false;
            };
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            _moveTransform.Move(_patrol.GetDirection());
        }
    }
}
