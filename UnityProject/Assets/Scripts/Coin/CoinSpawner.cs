using Modules;
using SnakeGame;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoinSpawner
{
    private Pool coinFactory;

    [Inject]
    public CoinSpawner(Pool coinFactory)
    {
        this.coinFactory = coinFactory;
    }

    public Coin SpawnCoin()
    {
        return coinFactory.Create();
    }

    public void DespawnCoin(Coin coin)
    {
        coinFactory.Destroy(coin);
    }

    public sealed class Pool : MonoMemoryPool<Coin>
    {
        private List<Vector2Int> occupiedPositions = new();
        private IWorldBounds worldBounds;

        [Inject]
        public Pool(IWorldBounds worldBounds) 
        {
            this.worldBounds = worldBounds;
        }

        public Coin Create()
        {
            Vector2Int position;
            do
            {
                position = worldBounds.GetRandomPosition();
            } while (occupiedPositions.Contains(position));

            Coin coin = Spawn();
            coin.Position = position;
            coin.Generate();
            return coin;
        }

        public void Destroy(Coin coin)
        {
            Despawn(coin);
        }
    }
}
