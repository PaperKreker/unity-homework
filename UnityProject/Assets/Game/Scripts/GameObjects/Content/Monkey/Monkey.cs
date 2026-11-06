using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(GroundedComponent))]
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(JumpComponent))]
    public class Monkey : MonoBehaviour
    {
        [SerializeField]
        private ZonePushComponent _zonePush;
        
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private JumpComponent _jump;

        private void Awake()
        {
            _grounded = GetComponent<GroundedComponent>();
            _health = GetComponent<HealthComponent>();
            _jump = GetComponent<JumpComponent>();
            
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
            
            _jump.JumpDelayed();
        }
    }
}
