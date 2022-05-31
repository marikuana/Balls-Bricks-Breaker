using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MainMenuLoad : ILoader
{
    public async Task Load()
    {
        var load = SceneManager.LoadSceneAsync(Scenes.MainMenu);
        while (!load.isDone)
            await Task.Delay(1);
        return;
    }
}
