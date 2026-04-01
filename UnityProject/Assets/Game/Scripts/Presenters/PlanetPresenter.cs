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
        private readonly PlanetPopupPresenter _planetPopupPresenter;
        private readonly PlanetView _planetView;
        private readonly Planet _planet;

        [Inject]
        public PlanetPresenter(
            PlanetPopupPresenter planetPopupPresenter, 
            PlanetView planetView, 
            Planet planet, 
            IMoneyAdapter moneyAdapter)
        {
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
            if (_moneyAdapter.IsEnough(_planet.Price))
            {
                _planet.Unlock();
                RefreshIncomeReady(_planet.IsIncomeReady);
            }
        }

        private void CollectIncome()
        {
            if (_planet.IsIncomeReady)
            {
                _planetView.GatherIncome();
                _planet.GatherIncome();
            }
        }

        private void RefreshLocked()
        {
            _planetView.SetLocked(!_planet.IsUnlocked, _planet.GetIcon(_planet.IsUnlocked));
        }

        private void RefreshIncomeTime(float remainingTime)
        {
            string remainingTimeText;

            int clampedTime = Mathf.Max(Mathf.CeilToInt(remainingTime), 0);
            remainingTimeText = $"{clampedTime / 60}m:{clampedTime % 60}s";

            _planetView.SetIncomeTime(remainingTimeText);
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
            [Inject]
            public Factory(PlanetView[] planetViews, Planet[] planets)
            {

            }

            public override PlanetPresenter Create(Planet planet, PlanetView planetView)
            {
                return base.Create(planet, planetView);
            }
        }
    }
}