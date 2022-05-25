using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    public ProgressData ProgressData { get; private set; }

    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        Instance = this;
        ProgressData = new ProgressData();
        LevelManager = new LevelManager();
    }
}
