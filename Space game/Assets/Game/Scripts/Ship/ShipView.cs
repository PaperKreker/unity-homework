using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField]
        private Ship _ship;

        [SerializeField]
        private ShipControllerViewConfig _viewConfig;

        [Header("Visual effects")]
        [SerializeField]
        private Transform _viewTransform;

        [SerializeField]
        private ParticleSystem _fireVFX;

        [SerializeField]
        private Renderer _renderer;

        [Header("Sound effects")]
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _fireSFX;

        [SerializeField]
        private AudioClip _damageSFX;

        private Material _material;
        private Tweener _damageAnimation;

        private void Awake()
        {
            _material = new Material(_viewConfig.MaterialPrefab);
            _renderer.material = _material;
        }

        private void OnEnable()
        {
            _ship.OnDamage += AnimateDamage;
            _ship.OnDead += AnimateDead;
            _ship.OnFire += AnimateFire;
        }

        private void OnDisable()
        {
            _ship.OnDamage -= AnimateDamage;
            _ship.OnDead -= AnimateDead;
            _ship.OnFire -= AnimateFire;
        }

        private void LateUpdate()
        {
            AnimateMovement(Time.deltaTime, _ship.Direction);
        }

        private void AnimateMovement(float deltaTime, Vector3 moveDirection)
        {
            Vector3 shipAngles = _viewTransform.localEulerAngles;
            shipAngles.x = _viewConfig.MoveRotationAngle * moveDirection.y;
            shipAngles.y = _viewConfig.MoveRotationAngle / 2 * moveDirection.x * -1f;

            Quaternion shipRotation = Quaternion.Euler(shipAngles);
            float t = _viewConfig.MoveSpeed * deltaTime;
            _viewTransform.localRotation = Quaternion.Lerp(_viewTransform.localRotation, shipRotation, t);
        }

        private void AnimateDamage()
        {
            if (_damageSFX)
                _audioSource.PlayOneShot(_damageSFX);

            if (_damageAnimation.IsActive())
                _damageAnimation.Kill();

            _damageAnimation = DOVirtual.Float(
                0f,
                1f,
                _viewConfig.HitDuration,
                progress => _material?.SetFloat(_viewConfig.HitPropertyName,
                    _viewConfig.HitAnimationCurve.Evaluate(progress))
            ).SetLink(_renderer.gameObject);
        }

        private void AnimateFire(Transform firePoint)
        {
            if (_fireSFX)
                _audioSource.PlayOneShot(_fireSFX);

            if (_fireVFX)
                _fireVFX.Play();
        }

        private void AnimateDead()
        {
            ParticleSystem prefab = _viewConfig.DestroyEffectPrefab;
            Instantiate(prefab, _viewTransform.position, prefab.transform.rotation);
        }
    }
}
