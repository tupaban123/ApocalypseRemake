using Apocalypse.CodeBase;
using Apocalypse.GameConstants;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Apocalypse.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public event Action<float> ProgressChanged;
        public event Action StartLoadScene;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadGame()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(Constants.GameSceneName));
        }

        public void LoadMenu()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(Constants.MenuSceneName));
        }

        public void Restart()
        {
            _coroutineRunner.StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            StartLoadScene?.Invoke();
            AsyncOperation loadingSceneProgress = SceneManager.LoadSceneAsync(Constants.LoadingSceneName);

            while (!loadingSceneProgress.isDone)
                yield return null;

            AsyncOperation sceneLoadProgress = SceneManager.LoadSceneAsync(sceneName);

            while(!sceneLoadProgress.isDone)
            {
                ProgressChanged?.Invoke(sceneLoadProgress.progress / .9f);
                yield return null;
            }
        }
    }
}