using Game.Views;
using Modules.Planets;
using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenterInstaller : IInitializable, IDisposable
    {
        private readonly Dictionary<string, Planet> _planetsByNames = new();
        private readonly PlanetView[] _planetViews;
        private readonly DiContainer _container;
        private readonly Planet[] _planets;
        private readonly List<PlanetPresenter> planetPresenters = new ();

        [Inject]
        public PlanetPresenterInstaller(DiContainer container, PlanetView[] planetViews, Planet[] planets)
        {
            _planetViews = planetViews;
            _container = container;
            _planets = planets;
        }

        public void Initialize()
        {
            foreach (Planet planet in _planets)
            {
                _planetsByNames[planet.Name] = planet;
            }

            foreach (PlanetView planetView in _planetViews)
            {
                PlanetPresenter planetPresenter = _container.Instantiate<PlanetPresenter>(new object[] {
                    _planetsByNames[planetView.GetName()],
                    planetView});

                planetPresenter.Initialize();
                planetPresenters.Add(planetPresenter);
            }
        }

        public void Dispose()
        {
            foreach (PlanetPresenter planetPresenter in planetPresenters)
            {
                planetPresenter.Dispose();
            }
        }
    }
}
