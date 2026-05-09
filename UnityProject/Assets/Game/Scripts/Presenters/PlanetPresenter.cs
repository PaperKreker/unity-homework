using Game.Gameplay;
using Game.Views;
using Modules.Planets;
using System;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyAdapter _moneyAdapter;
        private readonly CoinPresenter _coinPresenter;
        private readonly PlanetPopupPresenter _planetPopupPresenter;
        private readonly PlanetView _planetView;
        private readonly Planet _planet;

        public PlanetPresenter(
            CoinPresenter coinPresenter, 
            PlanetPopupPresenter planetPopupPresenter, 
            PlanetView planetView, 
            Planet planet, 
            IMoneyAdapter moneyAdapter)
        {
            _coinPresenter = coinPresenter;
            _planetPopupPresenter = planetPopupPresenter;
            _moneyAdapter = moneyAdapter;
            _planetView = planetView;
            _planet = planet;
        }

        public void Initialize()
        {
            _planetView.OnPlanetClicked += Interact;
            _planetView.OnPlanetHold += OpenPopup;

            _planet.OnIncomeTimeChanged += RefreshIncomeTime;
            _planet.OnIncomeReady += RefreshIncomeReady;
            _planet.OnUnlocked += RefreshLocked;
            _planet.OnUnlocked += RefreshUnlock;

            RefreshLocked();
            RefreshPrice();
        }

        public void Dispose()
        {
            _planetView.OnPlanetClicked -= Interact;
            _planetView.OnPlanetHold -= OpenPopup;

            _planet.OnIncomeTimeChanged -= RefreshIncomeTime;
            _planet.OnIncomeReady -= RefreshIncomeReady;
            _planet.OnUnlocked -= RefreshLocked;
            _planet.OnUnlocked -= RefreshUnlock;
        }

        public void OpenPopup()
        {
            if (_planet.IsUnlocked)
            {
                _planetPopupPresenter.OpenPopup(_planet);
            }
        }


        private void Interact()
        {
            if (_planet.IsUnlocked)
            {
                CollectIncome();
            }
            else
            {
                TryBuy();
            }
        }

        private void TryBuy()
        {
            _planet.Unlock();
        }

        private void CollectIncome()
        {
            if (_planet.IsIncomeReady)
            {
                _coinPresenter.SpawnCoin(_planetView.GetGatherPoint(), () =>
                {
                    _planet.GatherIncome();
                });
                _planetView.HideIncomeIcon();
            }
        }

        private void RefreshLocked()
        {
            _planetView.SetLocked(!_planet.IsUnlocked, _planet.GetIcon(_planet.IsUnlocked));
        }

        private void RefreshIncomeTime(float remainingTime)
        {
            int clampedTime = Mathf.Max(Mathf.CeilToInt(remainingTime), 0);
            string remainingTimeText = $"{clampedTime / 60}m:{clampedTime % 60}s";

            _planetView.SetIncomeTime(remainingTimeText);
            _planetView.SetIncomeProgressValue(_planet.IncomeProgress);
        }

        private void RefreshUnlock()
        {
            _planetView.SetIncomeReady(_planet.IsIncomeReady);
        }
        
        private void RefreshIncomeReady(bool isReady)
        {
            _planetView.SetIncomeReady(isReady);
        }

        private void RefreshPrice()
        {
            _planetView.SetPrice(_planet.Price.ToString());
        }

        public class Factory : PlaceholderFactory<Planet, PlanetView, PlanetPresenter>
        {
        }
    }
}