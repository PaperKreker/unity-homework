using Modules;
using System;
using Zenject;

public class GameCycle : IInitializable, IDisposable
{
    public event Action<bool> OnGameOver;
    public event Action<int> OnLevelUp;

    private SnakeInteraction snakeInteraction;
    private CoinController coinController;
    private IDifficulty difficulty;

    private int goal;

    [Inject]
    public GameCycle(SnakeInteraction snakeInteraction, CoinController coinController, IDifficulty difficulty)
    {
        this.snakeInteraction = snakeInteraction;
        this.coinController = coinController;
        this.difficulty = difficulty;
    }

    public void Initialize()
    {
        snakeInteraction.OnCollide += Lose;
        snakeInteraction.OnEatCoin += ChangeGoal;
    }

    public void Dispose()
    {
        snakeInteraction.OnCollide -= Lose;
        snakeInteraction.OnEatCoin -= ChangeGoal;
    }

    public void StartGame()
    {
        LevelUp();
    }

    public void LevelUp()
    {
        bool changeLevel = difficulty.Next(out int newLevel);
        if (!changeLevel)
        {
            Win();
        }
        else
        {
            goal = newLevel;
            coinController.SpawnCoins(newLevel);
            OnLevelUp?.Invoke(newLevel);
        }
    }

    private void ChangeGoal(Coin _)
    {
        --goal;
        if (goal == 0)
        {
            LevelUp();
        }
    }

    private void Win()
    {
        OnGameOver?.Invoke(true);
    }

    private void Lose()
    {
        OnGameOver?.Invoke(false);
    }
}
