using Game.Views;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class CoinPresenter
    {
        private readonly CoinView _coinView;

        public CoinPresenter(CoinView coinView)
        {
            _coinView = coinView;
        }

        public void SpawnCoin(Vector2 position, Action stopAction)
        {
            _coinView.SpawnCoin(position, stopAction);
        }

    }
}
