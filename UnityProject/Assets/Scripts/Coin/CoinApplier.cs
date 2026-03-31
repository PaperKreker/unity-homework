using Modules;
using System;
using Zenject;

public class CoinApplier : IInitializable, IDisposable
{
    private SnakeInteraction snakeInteraction;
    private ISnake snake;
    private IScore score;

    [Inject]
    public CoinApplier(SnakeInteraction snakeInteraction, ISnake snake, IScore score)
    {
        this.snakeInteraction = snakeInteraction;
        this.snake = snake;
        this.score = score;
    }

    public void Initialize()
    {
        snakeInteraction.OnEatCoin += ApplyCoin;
    }

    public void Dispose() 
    {
        snakeInteraction.OnEatCoin -= ApplyCoin;
    }

    private void ApplyCoin(Coin coin)
    {
        score.Add(coin.Score);
        snake.Expand(coin.Bones);
    }
}
