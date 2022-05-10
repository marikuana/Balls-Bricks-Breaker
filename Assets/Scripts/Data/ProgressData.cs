using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData
{
    public int Money { get; private set; }
    public int Balls { get; private set; }

    public event Action<int> OnMoneyUpdate; 

    public event Action<int> OnBallsUpdate;

    public ProgressData()
    {
        Money = PlayerPrefs.GetInt("money", 0);
        Balls = PlayerPrefs.GetInt("balls", 5);
    }

    private void SetMoney(int money)
    {
        Money = money;
        OnMoneyUpdate?.Invoke(money);
        SaveMoney();
    }

    public void AddMoney(int money)
    {
        SetMoney(money + Money);
    }

    public void Save()
    {
        SaveBalls();
        SaveMoney();
    }

    private void SaveMoney() =>
        PlayerPrefs.SetInt("money", Money);

    private void SaveBalls() =>
        PlayerPrefs.SetInt("balls", Balls);
}
