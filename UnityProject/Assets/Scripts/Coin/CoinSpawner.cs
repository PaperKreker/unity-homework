using Modules;
using SnakeGame;
using System;
using UnityEngine;
using Zenject;

public class CoinSpawner : IInitializable, IDisposable
{
    public event Action<Coin> OnSpawn;

    private Factory coinFactory;
    private IDifficulty difficulty;

    [Inject]
    public CoinSpawner(IDifficulty difficulty, Factory coinFactory)
    {
        this.coinFactory = coinFactory;
        this.difficulty = difficulty;
    }

    public void Initialize()
    {
        difficulty.OnStateChanged += SpawnCoins;
    }

    public void Dispose()
    {
        difficulty.OnStateChanged -= SpawnCoins;
    }

    public void SpawnCoins()
    {
        int coinsCount = difficulty.Current;

        for (int i = 0; i < coinsCount; i++)
        {
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        Coin coin = coinFactory.Create();
        OnSpawn?.Invoke(coin);
    }

    public void DespawnCoin(Coin coin)
    {
        GameObject.Destroy(coin.gameObject);
    }

    public sealed class Factory : PlaceholderFactory<Coin>
    {
        private IWorldBounds worldBounds;

        [Inject]
        public Factory(IWorldBounds worldBounds) 
        {
            this.worldBounds = worldBounds;
        }

        public override Coin Create()
        {
            Vector2Int position = worldBounds.GetRandomPosition();

            Coin coin = base.Create();
            coin.Position = position;
            coin.Generate();
            return coin;
        }
    }
}
