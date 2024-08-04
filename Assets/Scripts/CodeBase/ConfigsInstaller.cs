using Apocalypse.Configs.Player;
using UnityEngine;
using Zenject;

namespace Apocalypse.CodeBase
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle().NonLazy();
        }
    }
}