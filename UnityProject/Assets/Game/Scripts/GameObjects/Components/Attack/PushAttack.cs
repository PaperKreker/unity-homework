using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RequestComponent))]
    [RequireComponent(typeof(CooldownComponent))]
    [RequireComponent(typeof(RaycastComponent))]
    [RequireComponent(typeof(PushComponent))]
    public class PushAttack : MonoBehaviour
    {
        [SerializeField] 
        private Transform _firePoint;
        
        private CooldownComponent _cooldown;
        private RequestComponent _request;
        private RaycastComponent _raycast;
        private PushComponent _push;

        private void Awake()
        {
            _cooldown = GetComponent<CooldownComponent>();
            _raycast = GetComponent<RaycastComponent>();
            _request = GetComponent<RequestComponent>();
            _push = GetComponent<PushComponent>();
            
            _request.SetCondition(() => _cooldown.IsExpired);
            _request.SetAction(() =>
            {
                _cooldown.Reset();
                _push.Push(GetTarget());
            });
        }

        private Rigidbody2D GetTarget()
        {
            return _raycast.Cast(_firePoint.position, _firePoint.right);
        }
    }
}