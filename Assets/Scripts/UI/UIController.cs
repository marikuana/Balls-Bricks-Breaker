using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text bulletCount;
    [SerializeField]
    private TextMeshProUGUI moneyLabel;

    [SerializeField]
    private Stars stars;
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private Menu menu;

    private void OnDestroy()
    {
        Manager.Instance.ProgressData.OnMoneyUpdate -= SetMoney;
        Controller.Instance.OnStarChange -= stars.SetStars;
    }

    private void Start()
    {
        menuButton.onClick.AddListener(ShowMenu);

        SetMoney(Manager.Instance.ProgressData.Money);
        Manager.Instance.ProgressData.OnMoneyUpdate += SetMoney;

        Controller.Instance.OnStarChange += stars.SetStars;
    }

    private void Update()
    {
        SetBulletCount(0);
    }

    private void SetBulletCount(int value)
    {
        bulletCount.text = $"Bullets: {value}";
    }

    private void SetMoney(int money)
    {
        moneyLabel.text = money.ToString();
    }

    private void ShowMenu()
    {
        menu.Toggle();
    }

    private void SetStars(int count) =>
        stars.SetStars(count);
}
