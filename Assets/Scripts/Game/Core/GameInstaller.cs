using Game.Core.Resources.Manager;
using Game.Core.Session;
using Zenject;

namespace Game.Core
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameSession>().To<GameSession>().AsSingle();
            Container.Bind<IResourceManager>().To<ResourceManager>().AsSingle();
        }
    }
}