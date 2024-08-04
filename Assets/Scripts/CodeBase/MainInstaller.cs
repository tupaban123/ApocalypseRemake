using Apocalypse.SceneLoading;
using Zenject;

namespace Apocalypse.CodeBase
{
    public class MainInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle().NonLazy();
        }
    } 
}