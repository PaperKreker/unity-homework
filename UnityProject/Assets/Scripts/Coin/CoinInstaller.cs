using Modules;
using UnityEngine;
using Zenject;

public class CoinInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject coinPrefab;

    [SerializeField]
    private Transform container;

    public override void InstallBindings()
    {
        Container
            .BindFactory<Coin, CoinSpawner.Factory>()
            .FromComponentInNewPrefab(coinPrefab)
            .UnderTransform(container)
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<CoinSpawner>()
            .FromNew()
            .AsSingle();
    }
}
