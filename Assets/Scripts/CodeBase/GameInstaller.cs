using Zenject;
using UnityEngine;
using Apocalypse.InputSystem;
using Apocalypse.Player;

namespace Apocalypse.CodeBase.GameInstallers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("UI")]
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private Joystick _attackJoystick;

        [Header("Player")]
        [SerializeField] private PlayerView _playerPrefab;
        [SerializeField] private Transform _playerSpawnpoint;

        public override void InstallBindings()
        {
            InstallUI();
            InstallInputSystem();

            InstallPlayer();
        }

        private void InstallUI()
        {
            Container.Bind<IMoveJoystick>().FromInstance(_moveJoystick).AsSingle().NonLazy();
            Container.Bind<IAttackJoystick>().FromInstance(_attackJoystick).AsSingle().NonLazy();
        }

        private void InstallInputSystem()
        {
            Container.BindInterfacesAndSelfTo<TouchInputSystem>().FromNew().AsSingle().NonLazy();
        }

        private void InstallPlayer()
        {
            var player = Container.InstantiatePrefabForComponent<PlayerView>(_playerPrefab, _playerSpawnpoint.position, Quaternion.identity, null);
            Container.Bind<PlayerView>().FromInstance(player).AsSingle().NonLazy();
        }
    }
}