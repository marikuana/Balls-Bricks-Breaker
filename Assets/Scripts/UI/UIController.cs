using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private Controller controller;

    [SerializeField]
    private Text bulletCount;
    [SerializeField]
    private Text moneyLabel;

    [SerializeField]
    private Button restartLevel;
    [SerializeField]
    private Button pause;

    private void Awake()
    {
        SetMoney(Manager.Instance.ProgressData.Money);
        Manager.Instance.ProgressData.OnMoneyUpdate += SetMoney;
    }

    private void OnDestroy()
    {
        Manager.Instance.ProgressData.OnMoneyUpdate -= SetMoney;
    }

    private void Start()
    {
        restartLevel.onClick.AddListener(RestartLevel);
        pause.onClick.AddListener(Pause);
    }

    private void Update()
    {
        SetBulletCount(Launcher.Balls);
    }

    public void RestartLevel()
    {
        controller.RestartLevel();
    }

    public void Pause()
    {
        controller.TogglePause();
    }

    private void SetBulletCount(int value)
    {
        bulletCount.text = $"Bullets: {value}";
    }

    private void SetMoney(int money)
    {
        moneyLabel.text = money.ToString();
    }
}
