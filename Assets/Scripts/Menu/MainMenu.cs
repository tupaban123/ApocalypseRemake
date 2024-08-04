using Apocalypse.SceneLoading;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void Play()
    {
        _sceneLoader.LoadGame();
    }
}
