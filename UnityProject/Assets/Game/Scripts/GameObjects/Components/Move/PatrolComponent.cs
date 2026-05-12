using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PatrolComponent : MonoBehaviour
    {
        [SerializeField] 
        private List<Transform> _wayPoints;

        [SerializeField] 
        private float _changeDistance;

        private int _index;

        public Vector3 GetDirection()
        {
            if (_wayPoints.Count == 0)
                return Vector3.zero;
            
            Vector3 delta = _wayPoints[_index].position - transform.position;
            if (delta.magnitude <= _changeDistance)
            {
                _index = (_index + 1) % _wayPoints.Count;
                delta = _wayPoints[_index].position - transform.position;
            }
            
            return delta.normalized;
        }
    }
}