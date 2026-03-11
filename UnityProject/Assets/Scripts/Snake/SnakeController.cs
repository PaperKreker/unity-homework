using Modules;
using SnakeGame;
using System;
using UnityEngine;
using Zenject;

public class SnakeController : IInitializable, IDisposable, ITickable
{
    private GameOverDetector gameOverDetector;
    private IWorldBounds worldBounds;
    private ISnake snake;

    private PlayerInput playerInput = new ();

    [Inject]
    public SnakeController(ISnake snake, IWorldBounds worldBounds, GameOverDetector gameOverDetector)
    {
        this.gameOverDetector = gameOverDetector;
        this.worldBounds = worldBounds;
        this.snake = snake;
    }

    public void Initialize()
    {
        playerInput.OnRight += TurnRight;
        playerInput.OnLeft += TurnLeft;
        playerInput.OnDown += TurnDown;
        playerInput.OnUp += TurnUp;

        snake.OnSelfCollided += StopGame;
        snake.OnMoved += CheckBounds;

        gameOverDetector.OnGameOver += StopSnake;
    }

    public void Dispose()
    {
        playerInput.OnRight -= TurnRight;
        playerInput.OnLeft -= TurnLeft;
        playerInput.OnDown -= TurnDown;
        playerInput.OnUp -= TurnUp;

        snake.OnSelfCollided -= StopGame;
        snake.OnMoved -= CheckBounds;

        gameOverDetector.OnGameOver -= StopSnake;
    }

    public void Tick()
    {
        playerInput.Update();
    }

    private void TurnLeft()
    {
        snake.Turn(SnakeDirection.LEFT);
    }

    private void TurnUp()
    {
        snake.Turn(SnakeDirection.UP);
    }

    private void TurnRight()
    {
        snake.Turn(SnakeDirection.RIGHT);
    }

    private void TurnDown()
    {
        snake.Turn(SnakeDirection.DOWN);
    }

    private void CheckBounds(Vector2Int position)
    {
        if (!worldBounds.IsInBounds(position))
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        gameOverDetector.Lose();
    }

    private void StopSnake(bool _)
    {
        snake.SetActive(false);
    }
}
