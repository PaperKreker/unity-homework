using System;

public class GameOverDetector
{
    public event Action<bool> OnGameOver;

    public void Win()
    {
        OnGameOver?.Invoke(true);
    }

    public void Lose()
    {
        OnGameOver?.Invoke(false);
    }
}
