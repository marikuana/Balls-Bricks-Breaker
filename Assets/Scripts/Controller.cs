using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    [SerializeField]
    private Launcher launcher;

    private Level currentLevel;
    private List<GameObject> levelObjects = new List<GameObject>();

    private int star;
    public event Action<int> OnStarChange;

    public bool Pause { get; set; } = false;

    private void Awake()
    {
        Instance = this;

        launcher.OnLaunchBall += DecrementStar;
    }

    private void Destroy()
    {
        launcher.OnLaunchBall -= DecrementStar;
    }

    public void Init(Level level)
    {
        currentLevel = level;

        RestartLevel();
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
        SetStar(4);
        launcher.SetBalls(currentLevel.balls);
    }

    public void SetStar(int count)
    {
        star = Mathf.Clamp(count, 0, 4);
        OnStarChange?.Invoke(star);
    }

    private void DecrementStar()
    {
        SetStar(--star);
    }

    public void LoadLevel(Level level)
    {
        UnloadLevel();

        foreach (var obj in level.objects)
        {
            GameObject gameObject = Instantiate(level.Pref).gameObject;
            gameObject.transform.position = obj.Position;
            levelObjects.Add(gameObject);
        }
    }

    public void UnloadLevel()
    {
        foreach (var gameObject in levelObjects.ToList())
        {
            if (gameObject != null)
                Destroy(gameObject);
        }
        levelObjects.Clear();
    }
}
