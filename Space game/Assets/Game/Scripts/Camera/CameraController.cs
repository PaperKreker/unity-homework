using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private ShipHealth _playerHealth;

        [SerializeField]
        private CameraShaker _cameraShaker;

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += ShakeCamera;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= ShakeCamera;
        }

        private void ShakeCamera(int _)
        {
            _cameraShaker.Shake();
        }
    }
}
