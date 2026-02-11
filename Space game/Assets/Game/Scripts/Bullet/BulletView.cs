using UnityEngine;

namespace Game
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField]
        private BulletController bulletController;

        [SerializeField]
        private BulletViewConfig _configView;

        [SerializeField]
        private GameObject _blueVFX;

        [SerializeField]
        private GameObject _redVFX;

        private void OnEnable()
        {
            bulletController.OnHit += PlayHitVFX;
            bulletController.OnRespawn += RefreshVFX;
        }

        private void OnDisable()
        {
            bulletController.OnHit -= PlayHitVFX;
            bulletController.OnRespawn -= RefreshVFX;
        }

        private void RefreshVFX()
        {
            if (bulletController.Team == TeamType.Player)
            {
                _blueVFX.SetActive(true);
                _redVFX.SetActive(false);
            }
            else
            {
                _blueVFX.SetActive(false);
                _redVFX.SetActive(true);
            }
        }

        private void PlayHitVFX()
        {
            GameObject prefab = _configView.ExplosionVFX;
            Instantiate(prefab, transform.position, prefab.transform.rotation);
        }
    }
}
