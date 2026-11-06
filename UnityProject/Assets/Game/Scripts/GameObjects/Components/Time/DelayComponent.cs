using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class DelayComponent
    {
        public event Action OnExpire;
        
        [SerializeField] private float _duration;

        private bool _expired = true;

        public IEnumerator StartDelay()
        {
            if (!_expired)
                yield break;

            _expired = false;
            yield return new WaitForSeconds(_duration);
            _expired = true;
            OnExpire?.Invoke();
        }
    }
}