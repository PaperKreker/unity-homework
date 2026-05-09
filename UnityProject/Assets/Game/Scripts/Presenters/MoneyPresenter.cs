using Game.Views;
using Modules.Money;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _moneyView;

        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView moneyView)
        {
            _moneyStorage = moneyStorage;
            _moneyView = moneyView;
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += RefreshMoney;
            _moneyView.SetMoney(_moneyStorage.Money);
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= RefreshMoney;
        }

        private void RefreshMoney(int newValue, int prevValue)
        {
            _moneyView.SetMoney(newValue, prevValue);
        }
    }
}