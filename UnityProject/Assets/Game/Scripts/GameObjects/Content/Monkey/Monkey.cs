using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GroundedComponent))]
    [RequireComponent(typeof(CooldownComponent))]
    [RequireComponent(typeof(RequestComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(JumpComponent))]
    public class Monkey : MonoBehaviour
    {
        [SerializeField]
        private ZonePush _zonePush;
        
        private RequestComponent _jumpRequest;
        private CooldownComponent _cooldown;
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private JumpComponent _jump;

        private void Awake()
        {
            _jumpRequest = GetComponent<RequestComponent>();
            _cooldown = GetComponent<CooldownComponent>();
            _grounded = GetComponent<GroundedComponent>();
            _health = GetComponent<HealthComponent>();
            _jump = GetComponent<JumpComponent>();

            _jumpRequest.SetCondition(() => _cooldown.IsExpired && _grounded.IsGrounded);
            _jumpRequest.SetAction(() =>
            {
                _jump.Jump();
                _cooldown.Reset();
            });
            
            _health.OnDied += () =>
            {
                GetComponent<Rigidbody2D>().simulated = false;
            };

            _grounded.OnGrounded += isGrounded =>
            {
                if (isGrounded)
                {
                    _zonePush.Push();
                }
            };
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            _jumpRequest.Request();
        }
    }
}
