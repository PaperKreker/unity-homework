using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(MoveTransformComponent))]
    [RequireComponent(typeof(PatrolComponent))]
    public class Platform : MonoBehaviour
    {
        private MoveTransformComponent _moveTransform;
        private PatrolComponent _patrol;

        private void Awake()
        {
            _moveTransform = GetComponent<MoveTransformComponent>();
            _patrol = GetComponent<PatrolComponent>();
        }

        private void FixedUpdate()
        {
            _moveTransform.Move(_patrol.GetDirection());
        }
    }
}
