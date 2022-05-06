using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Bullet bulletPref;
    [SerializeField] private float bulletPerSecond = 10f;

    public static int Balls = 0;

    [SerializeField] private int ballCount = 5;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        Vector3 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = transform.position.z;
        Vector3 vector = Vector3.Normalize(cursorPos - transform.position);
        if (vector.y <= 0)
            return;
        
        lineRenderer.SetPosition(1, vector * Vector3.Distance(transform.position, cursorPos));

        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot(vector));
        }
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

}
