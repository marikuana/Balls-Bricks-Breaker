using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData
{
    public int Money { get; private set; }
    public int Balls { get; private set; }

    private Dictionary<string, int> levelStars;

    public event Action<int> OnMoneyUpdate; 

    public event Action<int> OnBallsUpdate;

    public ProgressData()
    {
        Money = PlayerPrefs.GetInt("money", 0);
        Balls = PlayerPrefs.GetInt("balls", 5);
        levelStars = JsonUtility.FromJson<Dictionary<string, int>>(PlayerPrefs.GetString("levelProgress", "{}"));
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

    public void SetLevelStar(string levelId, int star)
    {
        if (levelStars.ContainsKey(levelId))
            levelStars[levelId] = star;
        else
            levelStars.Add(levelId, star);
        SaveLevelStars();
    }

    public int GetLevelStar(Level level)
    {
        return GetLevelStar(level.LevelId);
    }

    public int GetLevelStar(string levelId)
    {
        if (levelStars.ContainsKey(levelId))
            return levelStars[levelId];
        return 0;
    }

    public void Save()
    {
        SaveBalls();
        SaveMoney();
        SaveLevelStars();
    }

    private void SaveMoney() =>
        PlayerPrefs.SetInt("money", Money);

    private void SaveBalls() =>
        PlayerPrefs.SetInt("balls", Balls);

    private void SaveLevelStars() =>
        PlayerPrefs.SetString("levelProgress", JsonUtility.ToJson(levelStars));
}
