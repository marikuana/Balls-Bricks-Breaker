using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Vector3 movement;
    private Vector3 spawnPosition;
    [SerializeField] private float destroyDistance = 10f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Awake()
    {
        spawnPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       /* if (rb.velocity.normalized.y == 0)
            rb.velocity = new Vector2(rb.velocity.x, -0.01f);*/
        rb.velocity = rb.velocity.normalized * speed;

        if (Vector2.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Destroy();
        }
    }


    public void SetMovement(Vector3 movement)
    {
        rb.velocity = new Vector2();
        rb.AddForce(movement);
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private void Destroy()
    {
        Gun.Balls--;
        Destroy(gameObject);
    }

}
