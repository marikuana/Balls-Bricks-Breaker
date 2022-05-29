using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData
{
    public int Money { get; private set; }

    private Dictionary<string, int> levelStars;

    public event Action<int> OnMoneyUpdate; 

    public ProgressData()
    {
        Money = PlayerPrefs.GetInt("money", 0);
        levelStars = Deserialize<Dictionary<string, int>>(PlayerPrefs.GetString("levelProgress", "{}"));
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

    public void SetLevelStar(Level level, int star)
    {
        SetLevelStar(level.LevelId, star);
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

    public bool IsLevelComplite(Level level) =>
        levelStars.ContainsKey(level.LevelId);

    public void Save()
    {
        SaveMoney();
        SaveLevelStars();
    }

    private void SaveMoney() =>
        PlayerPrefs.SetInt("money", Money);

    private void SaveLevelStars()
    {
        PlayerPrefs.SetString("levelProgress", Serialize(levelStars));
    }

    private string Serialize(object obj) =>
        JsonConvert.SerializeObject(obj);

    private T Deserialize<T>(string json) =>
        JsonConvert.DeserializeObject<T>(json);
}
