using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button startGame;
    [SerializeField]
    private Button chooseLevel;
    [SerializeField]
    private Button quitGame;
    [SerializeField]
    private LevelList levelList;

    private void Awake()
    {
        startGame.onClick.AddListener(StartGame);
        chooseLevel.onClick.AddListener(ChooseLevel);
        quitGame.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        new LevelLoader(Manager.Instance.GetLastLevel())
            .Load();
    }

    public void ChooseLevel()
    {
        levelList.OnClickOpen();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
