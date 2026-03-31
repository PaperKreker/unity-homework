using Game.Views;
using Modules.Money;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly CoinPresenter _coinPresenter;
        private readonly IMoneyStorage _moneyStorage;
        private readonly PlanetView[] _planetViews;
        private readonly MoneyView _moneyView;
        private Vector2? _gatherPosition;

        [Inject]
        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView moneyView, CoinPresenter coinPresenter, PlanetView[] planetViews)
        {
            _moneyStorage = moneyStorage;
            _moneyView = moneyView;
            _coinPresenter = coinPresenter;
            _planetViews = planetViews;
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += RefreshMoney;
            _moneyView.SetMoney(_moneyStorage.Money);

            foreach (PlanetView planetView in _planetViews) {
                planetView.OnGather += SetGatherPosition;
            }
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= RefreshMoney;

            foreach (PlanetView planetView in _planetViews)
            {
                planetView.OnGather -= SetGatherPosition;
            }
        }

        private void RefreshMoney(int newValue, int prevValue)
        {
            if (_gatherPosition == null)
            {
                _moneyView.SetMoney(newValue, prevValue);
            }
            else
            {
                _coinPresenter.SpawnCoin(_gatherPosition.Value, () => RefreshMoney(newValue, prevValue));
                _gatherPosition = null;
            }
        }

        private void SetGatherPosition(Vector2 position)
        {
            _gatherPosition = position;
        }
    }
}