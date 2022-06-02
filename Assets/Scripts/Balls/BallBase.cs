using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BallBase : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    private Rigidbody2D rb;
    public float Damage = 1f;

    public Vector2 velocity = Vector2.zero;
    public PhysicBase Physic;

    void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        Physic = new Physic(this);
    }

    public BallBase Initialize(Vector3 position, Vector3 movement)
    {
        transform.position = position;
        SetMovement(movement);
        return this;
    }

    void Update()
    {
        /*if (rb.velocity.normalized != Vector2.zero)
            velocity = rb.velocity.normalized;

        if (velocity.y == 0f)
            velocity = new Vector2(velocity.x, Random.Range(0f, velocity.x / 2f)).normalized;

        rb.velocity = velocity * speed * Controller.Instance.SimulateSpeed;*/
        transform.position = Physic.CalculatePosition();

        if (transform.position.y < -5f)
        {
            Destroy();
        }
    }

    public void SetMovement(Vector3 movement)
    {
        velocity = movement;
        //rb.velocity = new Vector2();
        //rb.AddForce(movement * speed * Controller.Instance.SimulateSpeed, ForceMode2D.Impulse);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public abstract void Impact(Block block);
}
