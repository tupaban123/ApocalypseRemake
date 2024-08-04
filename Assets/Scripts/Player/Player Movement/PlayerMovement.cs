using Apocalypse.CodeBase;
using Apocalypse.Configs.Player;
using System;
using System.Collections;
using UnityEngine;

namespace Apocalypse.Player.Movement
{
    public class PlayerMovement
    { 
        private PlayerView _playerView;
        private Rigidbody _playerRb;
        private PlayerConfig _playerConfig;

        private Transform _transform;

        private ICoroutineRunner _coroutineRunner;

        private bool _isDashing = false;
        private bool _canDash = true;

        private Action OnStartDash;
        private Action OnEndDash;

        public PlayerMovement(PlayerView playerView, PlayerConfig playerConfig, Rigidbody playerRb)
        { 
            _playerView = playerView;
            _playerConfig = playerConfig;
            _playerRb = playerRb;

            _transform = playerRb.transform;

            _coroutineRunner = playerView;

            _playerView.OnJoysticksInput += ReadInput;

            OnStartDash += _playerView.DashStartCallback;
            OnEndDash += _playerView.DashEndCallback;
        }

        public void Dispose()
        {
            _playerView.OnJoysticksInput -= ReadInput;

            OnStartDash -= _playerView.DashStartCallback;
            OnEndDash -= _playerView.DashEndCallback;
        }

        private void ReadInput(Vector2 moveInput, Vector2 attackInput)
        {
            if (_isDashing)
                return;

            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            Vector3 attackDirection = new Vector3(attackInput.x, 0, attackInput.y);

            MovePlayer(moveDirection);

            if (moveInput == Vector2.zero && attackInput == Vector2.zero)
                return;

            Vector3 lookRotation = attackDirection == Vector3.zero ? moveDirection : attackDirection;

            RotatePlayer(lookRotation);
        }

        private void MovePlayer(Vector3 moveDirection)
        {
            float speed = _isDashing ?
            _playerConfig.Speed * _playerConfig.DashSpeedMultiplier :
            _playerConfig.Speed;

            Vector3 velocity = moveDirection * speed;

            _playerRb.linearVelocity = new Vector3(velocity.x, _playerRb.linearVelocity.y, velocity.z);
        }

        private void RotatePlayer(Vector3 lookRotation)
        {
            Quaternion rotateTarget = Quaternion.LookRotation(lookRotation);

            _transform.rotation = Quaternion.Lerp(_transform.rotation, rotateTarget, _playerConfig.RotateSpeed * Time.deltaTime);
        }

        private IEnumerator Dash()
        {
            _canDash = false;

            float t = .5f;
            
            while (t > 0f)
            {
                if (_playerView.MoveJoystickDistanceFromCenter == 1f)
                    t = 0;

                t -= Time.deltaTime;
                yield return null;
            }

            if (_playerView.MoveJoystickDistanceFromCenter < 1f)
            {
                _canDash = true;
                yield break;
            }

            _isDashing = true;
            OnStartDash?.Invoke();

            Vector3 currentMoveDirection = _playerRb.linearVelocity.normalized;

            MovePlayer(currentMoveDirection);

            yield return new WaitForSeconds(_playerConfig.DashDuration);

            _isDashing = false;
            OnEndDash?.Invoke();

            _coroutineRunner.StartCoroutine(ReloadDash());
        }

        private IEnumerator ReloadDash()
        {
            _canDash = false;
            _isDashing = false;

            yield return new WaitForSeconds(_playerConfig.DashCooldown);
            
            _canDash = true;
        }

        public void DoubleClickCallback()
        {
            if (_canDash)
                _coroutineRunner.StartCoroutine(Dash());
        }
    }
}
