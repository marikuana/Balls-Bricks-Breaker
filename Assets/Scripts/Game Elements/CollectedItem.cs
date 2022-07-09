using UnityEngine;

public abstract class CollectedItem : Item
{
    protected abstract void Collect();

    private void OnTriggerEnter2D(Collider2D collision) =>
        Collect();

}

