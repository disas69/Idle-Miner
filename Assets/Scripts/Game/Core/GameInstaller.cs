using Framework.UI.Structure;
using Game.Core.Resources.Manager;
using Game.Core.Session;
using Zenject;

namespace Game.Core
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameSession>().To<GameSession>().AsSingle().NonLazy();
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
            Container.Bind<INavigationProvider>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}