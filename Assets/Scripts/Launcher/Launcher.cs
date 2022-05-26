using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private float bulletPerSecond = 10f;

    public static int Balls = 0;

    private LineRenderer lineRenderer;

    public LaunchStatus status;

    public event Action OnLaunchBall;

    private List<BallBase> balls = new List<BallBase>();
    private List<BallType> ballQuery = new List<BallType>();

    [SerializeField]
    private BallFactory ballFactory;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        status = new ReadyToLaunch(this);
    }

    void Update()
    {
        status.Update();
    }

    public void SetBalls(params BallType[] balls)
    {
        ballQuery = balls.ToList();
    }

    public void StartLaunch(Vector2 launchVector, Action endShoot)
    {
        StartCoroutine(Shoot(launchVector, endShoot));
        OnLaunchBall?.Invoke();
    }

    private IEnumerator Shoot(Vector3 vector, Action endShoot)
    {
        foreach (var ball in ballQuery)
        {
            LaunchBall(ball, vector);

            float dealy = 0f;
            while (dealy < 1f / bulletPerSecond)
            {
                if (!Controller.Instance.Pause)
                    dealy += Time.deltaTime;
                yield return null;
            }
        }
        endShoot.Invoke();
    }

    private void LaunchBall(BallType ballType, Vector3 vector)
    {
        BallBase ball = Instantiate(ballFactory.GetBallPref(ballType), transform).Initialize(transform.position, vector);
        balls.Add(ball);
        Balls++;
    }

    public void RenderLine(bool visible) =>
        lineRenderer.forceRenderingOff = !visible;

    public void SetLineColor(Color color) =>
        lineRenderer.endColor = color;

    public void SetLinePoistion(Vector2 position) =>
        lineRenderer.SetPosition(1, position);
}
