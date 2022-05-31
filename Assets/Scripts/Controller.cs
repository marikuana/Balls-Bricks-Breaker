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
    private List<Block> levelOBlocks = new List<Block>();

    private int star;
    public event Action<int> OnStarChange;

    [SerializeField]
    private float simulateSpeed = 1f;
    [SerializeField]
    private bool pause = false;

    public float SimulateSpeed => pause ? 0f : simulateSpeed;

    private void Awake()
    {
        Instance = this;

        launcher.OnLaunchBall += DecrementStar;
    }

    private void Destroy()
    {
        launcher.OnLaunchBall -= DecrementStar;
    }

    private void Update()
    {
        if (currentLevel == null)
            return;

        if (levelOBlocks.Where(block => block != null).Count() == 0)
            CompleteLevel();
    }

    private void CompleteLevel()
    {
        if (Manager.Instance.ProgressData.GetLevelStar(currentLevel) < star)
            Manager.Instance.ProgressData.SetLevelStar(currentLevel, star);

        Initialize(Manager.Instance.LevelManager.GetNextLevel(currentLevel));
    }

    public void Initialize(Level level)
    {
        currentLevel = level;
        launcher.SetBalls(currentLevel.balls);
        RestartLevel();
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
        SetStar(4);
        
        launcher.Restart();
    }

    public void SetStar(int count)
    {
        star = Mathf.Clamp(count, 0, 4);
        OnStarChange?.Invoke(star);
    }

    public void SetPause(bool pause)
    {
        this.pause = pause;
    }

    public void SetSimulationSpeed(float speed)
    {
        simulateSpeed = speed;
    }

    private void DecrementStar()
    {
        SetStar(--star);
    }

    public void LoadLevel(Level level)
    {
        UnloadLevel();

        foreach (var obj in level.Blocks)
        {
            Block block = level.BlockFactory.Get(obj.Type);
            block.SetHealth(obj.Heath);
            block.SetPosition(obj.Position);
            levelOBlocks.Add(block);
        }
    }

    public void UnloadLevel()
    {
        foreach (var block in levelOBlocks.ToList())
        {
            if (block != null)
                Destroy(block.gameObject);
        }
        levelOBlocks.Clear();
    }
}
