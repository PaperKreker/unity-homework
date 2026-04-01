using Game.Views;
using Modules.Planets;
using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresentersInitializer : IInitializable, IDisposable
    {
        private readonly Dictionary<string, Planet> _planetsByNames = new();
        private readonly List<PlanetPresenter> planetPresenters = new ();

        private readonly PlanetPresenter.Factory _presenterFactory;
        private readonly PlanetView[] _planetViews;
        private readonly Planet[] _planets;

        [Inject]
        public PlanetPresentersInitializer(
            PlanetPresenter.Factory presenterFactory, 
            PlanetView[] planetViews, 
            Planet[] planets)
        {
            _presenterFactory = presenterFactory;
            _planetViews = planetViews;
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
                Planet planet = _planetsByNames[planetView.GetName()];
                PlanetPresenter planetPresenter = _presenterFactory.Create(
                    planet,
                    planetView);

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
