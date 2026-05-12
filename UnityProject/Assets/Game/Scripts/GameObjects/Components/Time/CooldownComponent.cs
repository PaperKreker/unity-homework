using UnityEngine;

namespace Game
{
    public class CooldownComponent : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private float _currentTime;

        public bool IsExpired => Time.time - _currentTime >= _duration;

        private void Awake()
        {
            _currentTime = Time.time - _duration;
        }

        public void Reset()
        {
            _currentTime = Time.time;
        }
    }
}