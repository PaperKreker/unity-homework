using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TakeDamageColorComponent))]
    public class SnakeView : MonoBehaviour
    {
        [SerializeField] 
        private Animator _animator;
        
        [SerializeField] 
        private Snake _snake;

        private TakeDamageColorComponent _takeDamageColor;
        private TargetFinderComponent _targetFinder;
        private GroundedComponent _grounded;
        private HealthComponent _health;
        private LookComponent _look;
        private float lastMoveDirection;
        
        private readonly int _isGroundedKey = Animator.StringToHash("IsGrounded");
        private readonly int _isMovingKey = Animator.StringToHash("IsMoving");
        private readonly int _deathKey = Animator.StringToHash("Death");

        private void Awake()
        {
            _targetFinder = _snake.GetComponent<TargetFinderComponent>();
            _grounded = _snake.GetComponent<GroundedComponent>();
            _health = _snake.GetComponent<HealthComponent>();
            _look = _snake.GetComponent<LookComponent>();
            
            _takeDamageColor = GetComponent<TakeDamageColorComponent>();

            _health.OnDied += () =>
            {
                _animator.SetTrigger(_deathKey);
            };

            _health.OnHealthChanged += (_) =>
            {
                _takeDamageColor.TakeDamage();
            };
        }

        private void FixedUpdate()
        {
            if (_health.CurrentHealth == 0)
                return;
            
            RefreshAnimator();
            RefreshLook();
        }

        private void RefreshAnimator()
        {
            _animator.SetBool(_isMovingKey, _snake.IsMoving);
            _animator.SetBool(_isGroundedKey, _grounded.IsGrounded);
        }
        
        private void RefreshLook()
        {
            if (_targetFinder.Target != null)
            {
                _look.Look(_targetFinder.Target.transform);
            }
        }
    }
}