using Apocalypse.CodeBase;
using Apocalypse.Configs.Player;
using Apocalypse.Entity;
using Apocalypse.InputSystem;
using System;
using UnityEngine;
using Zenject;

namespace Apocalypse.Player
{
    public class PlayerView : MonoBehaviour, ICoroutineRunner, IDamageable
    {
        [SerializeField] private Rigidbody _playerRb;
        [SerializeField] private TrailRenderer _dashTrailRenderer;

        private IInputSystem _inputSystem;

        private PlayerModel _playerModel;
        private PlayerConfig _playerConfig;

        private Transform _transform;
        public Transform Transform => _transform;

        public Action<Vector2, Vector2> OnJoysticksInput;

        public Action DashStartCallback => OnDashStart;
        public Action DashEndCallback => OnDashEnd;

        public float MoveJoystickDistanceFromCenter => Vector3.Distance(Vector3.zero, _inputSystem.MoveAxis);

        public EntityType EntityType => EntityType.Player;

        [Inject]
        private void Construct(IInputSystem inputSystem, PlayerConfig playerConfig)
        {
            _transform = transform;
            _inputSystem = inputSystem;
            _playerConfig = playerConfig;
        }

        private void Start()
        {
            _playerRb ??= GetComponent<Rigidbody>();
            _dashTrailRenderer ??= GetComponent<TrailRenderer>();

            _playerModel = new PlayerModel(this, _playerConfig, _playerRb);

            _inputSystem.DoubleClick += _playerModel.DoubleClickCallback;
        }

        private void OnDisable()
        {
            _inputSystem.DoubleClick -= _playerModel.DoubleClickCallback;

            _playerModel.Dispose();
        }

        private void Update()
        {
            Vector2 moveJoystickInput = new Vector2(_inputSystem.MoveAxis.x, _inputSystem.MoveAxis.y);
            Vector2 attackJoystickInput = new Vector2(_inputSystem.AttackAxis.x, _inputSystem.AttackAxis.y);

            OnJoysticksInput?.Invoke(moveJoystickInput, attackJoystickInput);
        }

        private void OnDashStart() => _dashTrailRenderer.enabled = true;

        private void OnDashEnd() => _dashTrailRenderer.enabled = false;

        public void Damage(float damage)
        {
            _playerModel.Damage(damage);
        }
    }
}