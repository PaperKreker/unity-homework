using System;
using UnityEngine;

namespace Game
{
    public class RequestComponent : MonoBehaviour
    {
        public event Action OnInvoke;
        
        private Func<bool> _condition;
        private DelayComponent _delay;
        private Action _action;
        private bool _required;

        private void Awake()
        {
            _delay = GetComponent<DelayComponent>();
            if (_delay != null)
            {
                _delay.OnExpire += TryAct;
            }
        }

        public void SetCondition(Func<bool> condition) => _condition = condition;
        public void SetAction(Action action) => _action = action;
        
        public void Request()
        {
            _required = true;
        }

        private void FixedUpdate()
        {
            if (_required && (_condition == null || _condition.Invoke()))
            {
                if (_delay != null)
                {
                    _delay.StartDelay();
                }
                else
                {
                    Act();
                }
            }

            _required = false;
        }

        private void TryAct()
        {
            if (_condition == null || _condition.Invoke())
            {
                Act();
            }
        }
        
        private void Act()
        {
            _action?.Invoke();
            OnInvoke?.Invoke();
        }
    }
}