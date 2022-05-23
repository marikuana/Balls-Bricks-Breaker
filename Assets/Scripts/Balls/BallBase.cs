using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BallBase : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    public float Damage = 1f;

    private Vector2 velocity = Vector2.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector3 position, Vector3 movement)
    {
        transform.position = position;
        SetMovement(movement);
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero)
            velocity = rb.velocity.normalized;

        if (Controller.Instance.Pause)
            rb.velocity = Vector2.zero;
        else
            rb.velocity = velocity * speed;

        if (transform.position.y < -5f)
        {
            Destroy();
        }
    }

    public void SetMovement(Vector3 movement)
    {
        rb.velocity = new Vector2();
        rb.AddForce(movement);
    }

    protected void Destroy()
    {
        Launcher.Balls--;
        Destroy(gameObject);
    }

    public abstract void Impact(Block block);
}
