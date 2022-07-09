using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class Controller : MonoBehaviour
{
    public static Controller Instance { get; private set; }

    [SerializeField]
    private Launcher launcher;

    private int star;
    public event Action<int> OnStarChange;

    [SerializeField]
    private float simulateSpeed = 1f;
    [SerializeField]
    private bool pause = false;

    public float SimulateSpeed => pause ? 0f : simulateSpeed;

    private LevelController levelController;

    private void Awake()
    {
        Instance = this;
        levelController = new LevelController();
        launcher.OnLaunchBall += DecrementStar;
    }

    private void Destroy()
    {
        launcher.OnLaunchBall -= DecrementStar;
    }

    private void Update()
    {
        if (levelController.Complete())
            CompleteLevel();
    }

    private void CompleteLevel()
    {
        if (Manager.Instance.ProgressData.GetLevelStar(levelController.Level) < star)
            Manager.Instance.ProgressData.SetLevelStar(levelController.Level, star);

        Initialize(Manager.Instance.LevelManager.GetNextLevel(levelController.Level));
    }

    public void Initialize(Level level)
    {
        levelController.LoadLevel(level);
        launcher.Restart();
        launcher.SetBalls(level.balls);
        SetStar(4);
    }

    public void RestartLevel()
    {
        levelController.RestartLevel();
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
}
