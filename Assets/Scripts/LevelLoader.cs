using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader
{
    private Level level;

    public LevelLoader(Level level)
    {
        this.level = level;
    }

    public async void Load()
    {
        var loadScene = SceneManager.LoadSceneAsync(2);
        while (!loadScene.isDone)
            await Task.Delay(1);

        Scene scene = SceneManager.GetSceneByBuildIndex(2);
        Controller controller = GetController(scene.GetRootGameObjects());
        controller.Init(level);
    }

    private Controller GetController(GameObject[] gameObjects)
    {
        foreach (var obj in gameObjects)
        {
            if (obj.TryGetComponent(out Controller controller))
                return controller;
        }
        return null;
    }
}
