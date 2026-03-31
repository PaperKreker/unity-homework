using Zenject;

public sealed class GameContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<UIController>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<GameCycle>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<SnakeInteraction>()
            .FromNew()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<SnakeController>()
            .FromNew()
            .AsSingle();
    }
}
