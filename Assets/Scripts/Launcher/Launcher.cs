using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] public float bulletPerSecond = 10f;

    private LineRenderer lineRenderer;

    public LaunchStatus status;

    public event Action OnLaunchBall;

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

    private void Update()
    {
        status.Update();
    }

    public IEnumerable<BallType> GetBalls() =>
        ballQuery.ToList();

    public void SetBalls(params BallType[] balls) =>
        ballQuery = balls.ToList();

    public void LaunchBall()
    {
        OnLaunchBall?.Invoke();
    }

    public void Restart()
    {
        status.Restart();
    }

    public BallBase LaunchBall(BallType ballType, Vector3 vector)
    {
        return Instantiate(ballFactory.GetBallPref(ballType), transform).Initialize(transform.position, vector);
    }

    public void RenderLine(bool visible) =>
        lineRenderer.forceRenderingOff = !visible;

    public void SetLineColor(Color color) =>
        lineRenderer.endColor = color;

    public void SetLinePoistion(Vector2 position) =>
        lineRenderer.SetPosition(1, position);
}
