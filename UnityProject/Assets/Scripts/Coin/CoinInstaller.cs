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
            .BindMemoryPool<Coin, CoinSpawner.Pool>()
            .FromComponentInNewPrefab(coinPrefab)
            .UnderTransform(container)
            .AsSingle();

        Container
            .Bind<CoinSpawner>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<CoinController>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<CoinApplier>()
            .FromNew()
            .AsSingle();
    }
}
