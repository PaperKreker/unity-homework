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
                .Bind<ISerializer>()
                .To<EntityWorldSerializer>()
                .FromNew()
                .AsCached();
            
            Container
                .Bind<IRepository>()
                .To<WebRepository>()
                .FromNew()
                .AsSingle();

            Container
                .Bind<IEntitySerializer>()
                .To<EntitySerializer>()
                .FromNew()
                .AsSingle();
        }
    }
}