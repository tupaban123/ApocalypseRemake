using System;

namespace Apocalypse.SceneLoading
{
    public interface ISceneLoader
    {
        event Action<float> ProgressChanged;
        event Action StartLoadScene;

        void LoadMenu();
        void LoadGame();
        void Restart();
    }
}
