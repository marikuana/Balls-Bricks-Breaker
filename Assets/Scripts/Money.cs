using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Controller controller;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.ProgressData.AddMoney(1);
        Destroy(gameObject);
    }
}
