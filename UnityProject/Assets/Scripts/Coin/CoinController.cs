using Modules;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoinController
{
    private CoinSpawner coinSpawner;

    private List<Coin> activeCoins = new List<Coin>();

    [Inject]
    public CoinController(CoinSpawner coinSpawner)
    {
        this.coinSpawner = coinSpawner;
    }
    public void SpawnCoins(int count)
    {
        for (int i = 0; i < count; i++)
        {
            activeCoins.Add(coinSpawner.SpawnCoin());
        }
    }

    public bool TryDespawnAtPosition(Vector2Int position, out Coin coin)
    {
        for (int i = 0; i < activeCoins.Count; ++i)
        {
            if (activeCoins[i].Position == position)
            {
                coin = activeCoins[i];
                activeCoins.RemoveAt(i);

                coinSpawner.DespawnCoin(coin);

                return true;
            }
        }

        coin = null;
        return false;
    }
}
