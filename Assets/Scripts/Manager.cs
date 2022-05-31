using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager
{
    public static Manager Instance 
    { 
        get 
        { 
            if (instance == null) 
                instance = new Manager();
            return instance;
        } 
    }
    private static Manager instance;
    public ProgressData ProgressData { get; private set; }

    public LevelManager LevelManager { get; private set; }

    private Manager()
    {
        instance = this;
        ProgressData = new ProgressData();
        LevelManager = new LevelManager();
    }

    public Level GetLastLevel()
    {
        return LevelManager.GetLevels().FirstOrDefault(level => !ProgressData.IsLevelComplite(level)) ?? 
            LevelManager.GetLevels().Last();
    }
}
