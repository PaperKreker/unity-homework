using Modules;
using System;
using Zenject;

public class SnakeController : IInitializable, IDisposable, ITickable
{
    private GameCycle gameCycle;
    private ISnake snake;

    private PlayerInput playerInput = new ();

    [Inject]
    public SnakeController(GameCycle gameCycle, ISnake snake)
    {
        this.gameCycle = gameCycle;
        this.snake = snake;
    }

    public void Initialize()
    {
        gameCycle.OnLevelUp += ChangeSnakeSpeed;
        gameCycle.OnGameOver += StopSnake;
        playerInput.OnRotate += TurnSnake;
    }

    public void Dispose()
    {
        gameCycle.OnLevelUp -= ChangeSnakeSpeed;
        gameCycle.OnGameOver -= StopSnake;
        playerInput.OnRotate -= TurnSnake;
    }

    public void Tick()
    {
        playerInput.Update();
    }

    private void TurnSnake(SnakeDirection direction)
    {
        snake.Turn(direction);
    }

    private void ChangeSnakeSpeed(int speed)
    {
        snake.SetSpeed(speed);
    }

    private void StopSnake(bool _)
    {
        snake.SetActive(false);
    }
}
