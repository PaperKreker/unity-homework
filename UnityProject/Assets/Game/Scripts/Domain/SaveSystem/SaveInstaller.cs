using Zenject;

namespace SampleGame.SaveSystem
{
    public class SaveInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SaveManager>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<ISaveSerializer>()
                .To<EntitySerializer>()
                .FromNew()
                .AsCached();
        }
    }
}