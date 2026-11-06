using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class CooldownComponent
    {
        [SerializeField] private float _duration;

        private float _currentTime;

        public bool IsExpired => Time.time - _currentTime >= _duration;

        public void Reset()
        {
            _currentTime = Time.time;
        }
    }
}