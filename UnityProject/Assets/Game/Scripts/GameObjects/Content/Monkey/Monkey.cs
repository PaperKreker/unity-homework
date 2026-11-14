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
        }
        
        private void OnEnable()
        {
            _grounded.OnGrounded += PushOnLand;
            _health.OnDied += Die;
        }
        
        private void OnDisable()
        {
            _grounded.OnGrounded -= PushOnLand;
            _health.OnDied -= Die;
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            _jump.JumpDelayed();
        }

        private void PushOnLand(bool isGrounded)
        {
            if (isGrounded)
            {
                _zonePush.Push();
            }
        }
        
        private void Die()
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
}
