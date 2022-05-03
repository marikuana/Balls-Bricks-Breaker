using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 10f;

    public void Damage(int value)
    {
        health -= value;
        if (!Alive)
            Destroy();
    }

    public bool Alive => health > 0;

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
