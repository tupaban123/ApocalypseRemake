using Apocalypse.SceneLoading;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Apocalypse.LoadingUI
{
    public class LoadingProgressUI : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            _sceneLoader.ProgressChanged += OnProgressChanged;
        }

        private void OnDestroy()
        {
            if(_sceneLoader != null)
                _sceneLoader.ProgressChanged -= OnProgressChanged;
        }

        private void OnProgressChanged(float progress)
        {
            _progressBar.value = progress;
        }
    }
}