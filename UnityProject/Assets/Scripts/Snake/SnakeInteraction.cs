using Modules;
using SnakeGame;
using System;
using UnityEngine;
using Zenject;

public class SnakeInteraction : IInitializable, IDisposable
{
    public Action<Coin> OnEatCoin;
    public Action OnCollide;

    private CoinController coinController;
    private IWorldBounds worldBounds;
    private ISnake snake;

    [Inject]
    public SnakeInteraction(CoinController coinController, IWorldBounds worldBounds, ISnake snake)
    {
        this.coinController = coinController;
        this.worldBounds = worldBounds;
        this.snake = snake;
    }

    public void Initialize()
    {
        snake.OnSelfCollided += OnCollide;
        snake.OnMoved += CheckBounds;
        snake.OnMoved += TryEatCoin;
    }

    public void Dispose()
    {
        snake.OnSelfCollided -= OnCollide;
        snake.OnMoved -= CheckBounds;
        snake.OnMoved -= TryEatCoin;
    }

    private void CheckBounds(Vector2Int position)
    {
        if (!worldBounds.IsInBounds(position))
        {
            OnCollide?.Invoke();
        }
    }

    private void TryEatCoin(Vector2Int position)
    {
        if (coinController.TryDespawnAtPosition(position, out Coin coin))
        {
            OnEatCoin?.Invoke(coin);
        }
    }
}
