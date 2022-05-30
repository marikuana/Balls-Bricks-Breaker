using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : LaunchStatus
{
    private bool launching;
    private List<BallBase> balls;
    private Coroutine coroutine;

    public Launch(Launcher launcher, Vector3 shootDirection) : base(launcher)
    {
        launching = true;
        balls = new List<BallBase>();
        launcher.RenderLine(true);

        Start(shootDirection);
    }

    public override void Update()
    {
        if (launching == false)
        {
            launcher.RenderLine(false);
            launcher.status = new WaitBall(launcher, balls);
        }
    }

    public void Start(Vector2 launchVector)
    {
        coroutine = launcher.StartCoroutine(Shoot(launchVector));
        launcher.LaunchBall();
    }

    public IEnumerator Shoot(Vector3 vector)
    {
        foreach (var ball in launcher.GetBalls())
        {
            balls.Add(launcher.LaunchBall(ball, vector));

            float dealy = 0f;
            while (dealy < 1f / launcher.bulletPerSecond)
            {
                dealy += Time.deltaTime * Controller.Instance.SimulateSpeed;
                yield return null;
            }
        }
        EndLaunch();
    }

    public void EndLaunch()
    {
        launching = false;
    }

    public override void Restart()
    {
        launcher.StopCoroutine(coroutine);
        foreach (var ball in balls)
        {
            if (ball != null)
                ball.Destroy();
        }
        EndLaunch();
    }
}