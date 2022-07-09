using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : CollectedItem
{
    [SerializeField] private float rotateSpeed = 100f;
    private int _money;

    private void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    public void Initialize(Vector3 position, int money)
    {
        Initialize(position);
        _money = money;
    }

    protected override void Collect()
    {
        Manager.Instance.ProgressData.AddMoney(_money);
        Destroy(gameObject);
    }
}
