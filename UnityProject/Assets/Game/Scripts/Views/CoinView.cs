using Modules.UI;
using System;
using UnityEngine;

namespace Game.Views
{
    public class CoinView : MonoBehaviour
    {
        [SerializeField]
        private float _moveDuration = 0.5f;

        [SerializeField]
        private Transform _target;

        [SerializeField]
        private ParticleAnimator _coinAnimator;

        public void SpawnCoin(Vector3 position, Action stopAction)
        {
            _coinAnimator.Emit(position, _target.position, _moveDuration, stopAction);
        }
    }
}