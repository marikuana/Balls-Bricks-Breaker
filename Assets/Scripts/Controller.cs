using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    private Level currentLevel;
    private List<GameObject> levelObjects = new List<GameObject>();

    private int star;
    public event Action<int> OnStarChange;

    public bool Pause { get; set; } = false;

    private void Awake()
    {
        Instance = this;
        currentLevel = Manager.Instance.LevelManager.GetLevels()[2];
        SetStar(3);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
        SetStar(3);
    }

    public void SetStar(int count)
    {
        star = count;
        OnStarChange?.Invoke(star);
    }

    [MenuItem("Tool/Dec")]
    static void DecrementStar()
    {
        Instance.SetStar(--Instance.star);
    }

    [MenuItem("Tool/Dec", true)]
    static bool DecrementStarValidate() =>
        Instance != null;

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
    }
}
