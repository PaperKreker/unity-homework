using System;
using UnityEngine;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        public event Action OnAlert;

        [SerializeField]
        private float _minCooldown = 2;

        [SerializeField]
        private float _maxCooldown = 3;

        private float _spawnCooldown;
        private float _spawnTime;

        private void Start()
        {
            ResetCooldown();
        }

        private void FixedUpdate()
        {
            if (Check())
            {
                OnAlert?.Invoke();
            }
        }

        public void ResetCooldown()
        {
            _spawnCooldown = UnityEngine.Random.Range(_minCooldown, _maxCooldown);
            _spawnTime = Time.fixedTime;
        }

        public bool Check()
        {
            float time = Time.fixedTime;
            if (time - _spawnTime < _spawnCooldown)
                return false;
            return true;
        }
    }
}
