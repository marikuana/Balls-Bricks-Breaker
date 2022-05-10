using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Bullet bulletPref;
    [SerializeField] private float bulletPerSecond = 10f;

    public static int Balls = 0;

    [SerializeField] private int ballCount = 5;
    private LineRenderer lineRenderer;
    private Vector2 launchVector = Vector2.up;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        Vector2 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
        launchVector = (cursorPos - (Vector2)transform.position).normalized;

        float lineLenght = Mathf.Clamp(Vector3.Distance(transform.position, cursorPos), 2, 7);
        lineRenderer.SetPosition(1, launchVector * lineLenght);

        lineRenderer.forceRenderingOff = !Input.GetButton("Fire1");

        if (CanShoot())
        {
            if (Input.GetButtonUp("Fire1"))
                StartLaunch();

            lineRenderer.endColor = Color.yellow;
        }
        else
        {

            lineRenderer.endColor = Color.red;
        }
    }

    private void StartLaunch()
    {
        StartCoroutine(Shoot(launchVector));
    }

    private IEnumerator Shoot(Vector3 vector)
    {
        for (int i = 0; i < ballCount; i++)
        {
            LaunchBall(vector);
            yield return new WaitForSeconds(1f / bulletPerSecond);
        }
    }

    private void LaunchBall(Vector3 vector)
    {
        Bullet bullet = Instantiate(bulletPref);
        bullet.transform.position = transform.position;
        bullet.SetMovement(vector);
        Balls++;
    }

    private bool CanShoot() =>
        launchVector.y > 0.3f;

}
