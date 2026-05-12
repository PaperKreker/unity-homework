using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RequestComponent))]
    [RequireComponent(typeof(GroundedComponent))]
    [RequireComponent(typeof(CooldownComponent))]
    [RequireComponent(typeof(JumpComponent))]
    public class Jump : MonoBehaviour
    {
        private RequestComponent _request;
        private GroundedComponent _grounded;
        private CooldownComponent _cooldown;
        private JumpComponent _jump;

        private void Awake()
        {
            _request = GetComponent<RequestComponent>();
            _grounded = GetComponent<GroundedComponent>();
            _cooldown = GetComponent<CooldownComponent>();
            _jump = GetComponent<JumpComponent>();
            
            _request.SetCondition(() => _cooldown.IsExpired && _grounded.IsGrounded);
            _request.SetAction(() =>
            {
                _cooldown.Reset();
                _jump.Jump();
            });
        }
    }
}
