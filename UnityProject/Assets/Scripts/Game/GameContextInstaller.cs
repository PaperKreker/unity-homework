using Zenject;

public sealed class GameContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<GameOverDetector>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<UIController>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<GameController>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<SnakeController>()
            .FromNew()
            .AsSingle();

        // Нужен, чтобы правильно работал difficulty.Next(out _);
        Container
            .BindExecutionOrder<GameController>(1);
    }
}
