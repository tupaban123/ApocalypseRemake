using Apocalypse.Player;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Apocalypse.Camera
{
    public class CameraPlayerInit : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _cmCamera;

        [Inject]
        private void Construct(PlayerView playerView)
        {
            _cmCamera.Follow = playerView.transform;
        }
    }
}