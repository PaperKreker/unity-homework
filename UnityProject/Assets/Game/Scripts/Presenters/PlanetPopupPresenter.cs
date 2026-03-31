using Game.Gameplay;
using Game.Views;
using Modules.Planets;
using System;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyAdapter _moneyAdapter;
        private readonly PlanetPopupView _planetPopupView;
        private Planet _targetPlanet;

        [Inject]
        public PlanetPopupPresenter(PlanetPopupView planetPopupView, IMoneyAdapter moneyAdapter)
        {
            _planetPopupView = planetPopupView;
            _moneyAdapter = moneyAdapter;
        }

        public void Initialize()
        {
            _planetPopupView.OnUpgradeClicked += Upgrade;
            _planetPopupView.OnCloseClicked += ClosePopup;
        }

        public void Dispose()
        {
            _planetPopupView.OnUpgradeClicked -= Upgrade;
            _planetPopupView.OnCloseClicked -= ClosePopup;
        }

        public void OpenPopup(Planet planet)
        {
            _targetPlanet = planet;

            _targetPlanet.OnPopulationChanged += RefreshPopulation;
            _targetPlanet.OnIncomeChanged += RefreshIncome;
            _targetPlanet.OnUpgraded += RefreshLevel;

            _planetPopupView.Open();
            RefreshView();
        }

        private void ClosePopup()
        {
            _targetPlanet.OnPopulationChanged -= RefreshPopulation;
            _targetPlanet.OnIncomeChanged -= RefreshIncome;
            _targetPlanet.OnUpgraded -= RefreshLevel;
            _targetPlanet = null;

            _planetPopupView.Close();
        }

        private void RefreshView()
        {
            _planetPopupView.SetTitle(_targetPlanet.Name);
            _planetPopupView.SetAvatar(_targetPlanet.GetIcon(_targetPlanet.IsUnlocked));

            RefreshPopulation(_targetPlanet.Population);
            RefreshIncome(_targetPlanet.MinuteIncome);
            RefreshLevel(_targetPlanet.Level);
        }

        private void Upgrade()
        {
            if (_moneyAdapter.IsEnough(_targetPlanet.Price))
            {
                _targetPlanet.Upgrade();
            }
        }

        private void RefreshPopulation(int population)
        {
            _planetPopupView.SetPopulation($"Population: {population}");
        }

        private void RefreshIncome(int income)
        {
            _planetPopupView.SetIncome($"Income: {income} / sec");
        }

        private void RefreshLevel(int level)
        {
            _planetPopupView.SetLevel($"Level: {level}/{_targetPlanet.MaxLevel}");

            _planetPopupView.SetUpgradeButton(_targetPlanet.Price.ToString(), _targetPlanet.CanUpgrade);
            _planetPopupView.SetMaxLevel(_targetPlanet.IsMaxLevel);
        }
    }
}
