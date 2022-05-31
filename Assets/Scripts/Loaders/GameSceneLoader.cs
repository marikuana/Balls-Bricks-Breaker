using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : ILoader
{
    private Level level;

    public GameSceneLoader(Level level)
    {
        this.level = level;
    }

    public async Task Load()
    {
        var loadScene = SceneManager.LoadSceneAsync(Scenes.Game);
        while (!loadScene.isDone)
            await Task.Delay(1);

        Scene scene = SceneManager.GetSceneByName(Scenes.Game);
        Controller controller = GetRoot<Controller>(scene.GetRootGameObjects());
        controller.Initialize(level);
        return;
    }

    private T GetRoot<T>(GameObject[] gameObjects) where T : MonoBehaviour
    {
        foreach (var obj in gameObjects)
        {
            if (obj.TryGetComponent(out T controller))
                return controller;
        }
        return null;
    }
}
