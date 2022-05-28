using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Button _continue;
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button mainMenu;

    private void Start()
    {
        _continue.onClick.AddListener(Hide);
        restart.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(MainMenu);
    }

    public void Toggle()
    {
        if (gameObject.activeSelf)
            Hide();
        else
            Show();
    }

    public void Show()
    {
        Controller.Instance.Pause = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Controller.Instance.Pause = false;
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Controller.Instance.RestartLevel();
        Hide();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
