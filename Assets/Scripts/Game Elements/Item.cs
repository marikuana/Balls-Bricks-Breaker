﻿using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void Collect();

    private void OnTriggerEnter2D(Collider2D collision) =>
        Collect();

}
