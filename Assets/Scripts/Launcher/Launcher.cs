using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] public Camera camera;
    [SerializeField] private BallBase bulletPref;
    [SerializeField] private float bulletPerSecond = 10f;

    public static int Balls = 0;

    [SerializeField] private int ballCount = 5;
    private LineRenderer lineRenderer;

    public LaunchStatus status;

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

    public void StartLaunch(Vector2 launchVector, Action endShoot)
    {
        StartCoroutine(Shoot(launchVector, endShoot));
    }

    private IEnumerator Shoot(Vector3 vector, Action endShoot)
    {
        for (int i = 0; i < ballCount; i++)
        {
            LaunchBall(vector);
            yield return new WaitForSeconds(1f / bulletPerSecond);
        }
        endShoot.Invoke();
    }

    private void LaunchBall(Vector3 vector)
    {
        Instantiate(bulletPref).Initialize(transform.position, vector);
        Balls++;
    }

    public void RenderLine(bool visible) =>
        lineRenderer.forceRenderingOff = !visible;

    public void SetLineColor(Color color) =>
        lineRenderer.endColor = color;

    public void SetLinePoistion(Vector2 position) =>
        lineRenderer.SetPosition(1, position);
}
