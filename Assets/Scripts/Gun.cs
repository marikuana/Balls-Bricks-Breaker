using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform arrow;
    private float distance;
    [SerializeField] private Bullet bulletPref;
    [SerializeField] private float bulletPerSecond = 10f;
    [SerializeField] private bool autoShot = false;

    private DateTime lastShot = DateTime.MinValue;

    public static int Balls = 0;

    private void Awake()
    {
        distance = Vector2.Distance(arrow.transform.position, transform.position);
    }

    void Update()
    {
        //Debug.Log($"{Balls} | FPS: {1f / Time.deltaTime}");

        Vector3 cursorPos = camera.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = transform.position.z;
        Vector3 vector = Vector3.Normalize(cursorPos - transform.position);
        if (vector.y <= 0)
            return;
        arrow.position = (transform.position + (vector * distance));
        

        if ((autoShot || Input.GetButton("Fire1")) && CanShoot())
        {
            Bullet bullet = Instantiate(bulletPref);
            bullet.transform.position = transform.position;
            bullet.SetMovement(vector);
            //bullet.SetMovement(new Vector3(-0.741407600f, 0.671055000f, 0.00000000f));
            lastShot = DateTime.Now;
            Balls++;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            autoShot = !autoShot;
    }

    private bool CanShoot() =>
        lastShot < DateTime.Now.AddSeconds(-1f / bulletPerSecond);

}
