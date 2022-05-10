using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoney : MonoBehaviour
{
    [SerializeField] private Controller controller;
    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Start()
    {
        controller.ProgressData.OnMoneyUpdate += SetMoney;
        SetMoney(controller.ProgressData.Money);
    }

    private void SetMoney(int money)
    {
        label.text = money.ToString();
    }
}
