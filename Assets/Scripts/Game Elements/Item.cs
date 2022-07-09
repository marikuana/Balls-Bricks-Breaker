using UnityEngine;

public abstract class Item : MonoBehaviour, ITriggerable
{
    protected abstract void Collect();

    public void OnTrigger()
    {
        Collect();
    }
}
