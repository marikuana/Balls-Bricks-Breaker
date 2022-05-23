using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    public ProgressData ProgressData;

    public bool Pause { get; private set; } = false;

    private void Awake()
    {
        Instance = this;
        ProgressData = new ProgressData();
    }

    public void TogglePause()
    {
        Pause = !Pause;
    }
}
