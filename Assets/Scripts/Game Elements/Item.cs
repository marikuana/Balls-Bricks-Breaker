using UnityEngine;

public class Item : MonoBehaviour
{
    protected void Initialize(Vector3 position)
    {
        SetPosition(position);
    }

    protected void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}

