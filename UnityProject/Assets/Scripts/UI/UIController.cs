using Modules;
using SnakeGame;
using System;
using UnityEngine;
using Zenject;

public class UIController : IInitializable, IDisposable
{
    private GameOverDetector gameOverDetector;
    private IDifficulty difficulty;
    private IGameUI gameUI;
    private IScore score;

    [Inject]
    public UIController(GameOverDetector gameOverDetector, IDifficulty difficulty, IGameUI gameUI, IScore score)
    {
        this.gameOverDetector = gameOverDetector;
        this.difficulty = difficulty;
        this.gameUI = gameUI;
        this.score = score;
    }

    public void Initialize()
    {
        difficulty.OnStateChanged += RefreshDifficulty;
        gameOverDetector.OnGameOver += GameOver;
        score.OnStateChanged += RefreshScore;

        RefreshScore();
        RefreshDifficulty();
    }

    public void Dispose()
    {
        difficulty.OnStateChanged -= RefreshDifficulty;
        gameOverDetector.OnGameOver -= GameOver;
        score.OnStateChanged -= RefreshScore;
    }

    private void RefreshDifficulty()
    {
        gameUI.SetDifficulty(difficulty.Current, difficulty.Max);
    }

    private void RefreshScore()
    {
        RefreshScore(score.Current);
    }
    private void RefreshScore(int newScore)
    {
        gameUI.SetScore(newScore.ToString());
    }

    private void GameOver(bool win)
    {
        gameUI.GameOver(win);
    }
}
