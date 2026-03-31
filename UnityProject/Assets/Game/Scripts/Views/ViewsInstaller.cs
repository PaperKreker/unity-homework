using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField]
        private PlanetPopupView _planetPopupView;

        [SerializeField]
        private List<PlanetView> _planetView;

        [SerializeField]
        private MoneyView _moneyView;

        [SerializeField]
        private CoinView _coinView;

        public override void InstallBindings()
        {
            Container.
                Bind<PlanetPopupView>().
                FromInstance(_planetPopupView).
                AsSingle();

            foreach (PlanetView planetView in _planetView)
            {
                Container.
                    Bind<PlanetView>().
                    FromInstance(planetView).
                    AsCached();
            }

            Container.
                Bind<MoneyView>().
                FromInstance(_moneyView).
                AsSingle();

            Container.
                Bind<CoinView>().
                FromInstance(_coinView).
                AsSingle();
        }
    }
}