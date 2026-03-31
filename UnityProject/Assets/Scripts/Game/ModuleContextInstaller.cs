using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

public sealed class ModuleContextInstaller : MonoInstaller
{
    [SerializeField]
    private WorldBounds worldBounds;

    [SerializeField]
    private GameUI gameUI;

    [SerializeField]
    private Snake snake;

    public override void InstallBindings()
    {
        Container
            .Bind<IScore>()
            .To<Score>()
            .FromNew()
            .AsSingle();

        Container
            .Bind<IGameUI>()
            .To<GameUI>()
            .FromInstance(gameUI)
            .AsSingle();

        Container
            .Bind<IWorldBounds>()
            .To<WorldBounds>()
            .FromInstance(worldBounds)
            .AsSingle();

        Container
            .Bind<ISnake>()
            .To<Snake>()
            .FromInstance(snake)
            .AsSingle();
    }
}
