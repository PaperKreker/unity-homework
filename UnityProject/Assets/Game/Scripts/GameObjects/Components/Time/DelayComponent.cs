using System;
using UnityEngine;

namespace Game
{
    public class DelayComponent : MonoBehaviour
    {
        public event Action OnExpire;
        public bool IsExpired => _expired;
        
        [SerializeField] private float _duration;

        private float _currentTime;
        private bool _expired;

        private void Awake()
        {
            _expired = true;
        }

        private void FixedUpdate()
        {
            if (!_expired && Time.time - _currentTime >= _duration)
            {
                OnExpire?.Invoke();
                _expired = true;
            }
        }

        public void StartDelay()
        {
            if (_expired)
            {
                Reset();
            }
        }
        
        public void Reset()
        {
            _currentTime = Time.time;
            _expired = false;
        }
    }
}