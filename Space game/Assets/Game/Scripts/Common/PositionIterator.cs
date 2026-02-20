using Modules.Utils;
using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class PositionIterator
    {
        [SerializeField] 
        private Transform[] _points;

        private int _index;

        public void Shuffle() 
        {
            _points?.Shuffle();
        }

        public Vector3 Next()
        {
            if (_index >= _points.Length)
            {
                _points.Shuffle();
                _index = 0;
            }

            return _points[_index++].position;
        }
    }
}
