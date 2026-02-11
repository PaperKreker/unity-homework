using Modules.Utils;
using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerShip _playerShip;

        [SerializeField]
        private CameraShaker _cameraShaker;

        private void OnEnable()
        {
            _playerShip.OnHealthChanged += ShakeCamera;
        }

        private void OnDisable()
        {
            _playerShip.OnHealthChanged -= ShakeCamera;
        }

        private void ShakeCamera(int _)
        {
            _cameraShaker.Shake();
        }
    }
}
