using Modules;
using SnakeGame;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameController : IInitializable, IDisposable
{
    private GameOverDetector gameOverDetector;
    private CoinSpawner coinSpawner;
    private IDifficulty difficulty;
    private IScore score;
    private ISnake snake;

    private List<Coin> activeCoins = new List<Coin>();

    [Inject]
    public GameController(GameOverDetector gameOverDetector, CoinSpawner coinSpawner, IDifficulty difficulty, ISnake snake, IScore score)
    {
        this.gameOverDetector = gameOverDetector;
        this.coinSpawner = coinSpawner;
        this.difficulty = difficulty;
        this.snake = snake;
        this.score = score;
    }

    public void Initialize()
    {
        coinSpawner.OnSpawn += AddCoin;
        snake.OnMoved += CheckSnakePosition;

        difficulty.Next(out _);
    }

    public void Dispose()
    {
        coinSpawner.OnSpawn -= AddCoin;
        snake.OnMoved -= CheckSnakePosition;
    }

    private void CheckSnakePosition(Vector2Int position)
    {
        for (int i = 0; i < activeCoins.Count; i++)
        {
            if (activeCoins[i].Position == position)
            {
                EatCoin(activeCoins[i]);
                --i;
            }
        }
    }

    private void EatCoin(Coin coin)
    {
        snake.Expand(coin.Bones);
        score.Add(coin.Score);

        coinSpawner.DespawnCoin(coin);

        activeCoins.Remove(coin);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (activeCoins.Count == 0)
        {
            bool changeLevel = difficulty.Next(out int newLevel);
            if (!changeLevel)
            {
                gameOverDetector.Win();
            }
            else
            {
                snake.SetSpeed(newLevel);
            }
        }
    }

    private void AddCoin(Coin coin)
    {
        activeCoins.Add(coin);
    }
}
