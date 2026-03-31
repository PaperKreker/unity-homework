using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

public sealed class DifficultyContextInstaller : MonoInstaller
{
    [SerializeField]
    private DifficultyConfig difficultyConfig;

    public override void InstallBindings()
    {
        Container
            .Bind<IDifficulty>()
            .To<Difficulty>()
            .FromNew()
            .AsSingle()
            .WithArguments(difficultyConfig.MaxDifficulty);
    }
}
