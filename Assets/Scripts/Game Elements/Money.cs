using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Item
{
    [SerializeField] private float rotateSpeed = 100f;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    protected override void Collect()
    {
        Manager.Instance.ProgressData.AddMoney(1);
        Destroy(gameObject);
    }
}
