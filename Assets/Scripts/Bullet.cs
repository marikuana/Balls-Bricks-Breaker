using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Vector2 movement;
    private Vector3 spawnPosition;
    [SerializeField] private float destroyDistance = 10f;
    private SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Awake()
    {
        spawnPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        //rb.velocity = movement * speed;
        Vector2 offset = movement.normalized * (speed * Time.deltaTime);

        //rb.MovePosition(rb.position + offset);
        transform.position += (Vector3)offset;
       

        RaycastHit2D[] raycasts = Physics2D.CircleCastAll(transform.position, 0.1f, offset, 0.1f);
        if (raycasts.Length > 0)
        {
            if (raycasts[0].collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(1);
            }
            Debug.Log($"RayCasts: {raycasts.Length}");
            ChangeMovement(raycasts[0].normal);

            SetColor(Random.value > 0.5f ? Color.yellow : Color.blue);
        }

        //transform.Translate(movement * speed * Time.deltaTime);
        if (Vector3.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Destroy();
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            damageable.Damage(1);

        Debug.Log(collision.contacts.Length);
        IEnumerable<Vector2> normals = collision.contacts.Select(s => s.normal);
        Vector2 sum = new Vector2();
        foreach (var vector in normals)
        {
            sum.x += vector.x;
            sum.y += vector.y;
        }
        Vector2 normal = new Vector2(sum.x / normals.Count(), sum.y / normals.Count());
        //Vector2 normal = collision.contacts.First().normal;
        ChangeMovement(normal);

        SetColor(Random.value > 0.5f ? Color.yellow : Color.blue);
    }*/

    public void ChangeMovement(Vector3 normal)
    {
        SetMovement(Vector3.Reflect(movement, normal.normalized));
    }

    public void SetMovement(Vector3 movement)
    {
        this.movement = movement.normalized;
        //rb.velocity = movement * speed;
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
    
    private void OnDrawGizmosSelected()
    {
        int i = 0;
        Gizmos.color = Color.yellow;

        Vector3 position = transform.position;
        float distance = speed;
        Vector3 direction = movement.normalized;

        while (distance > 0f)
        {
            Vector2 normal = Vector2.zero;
            float disToHit = 0f;

            RaycastHit2D hit = Physics2D.CircleCast(position, 0.1f, direction, distance);
            if (hit != default(RaycastHit2D))
            {
                disToHit = hit.distance;
                normal = hit.normal;
            }
            else
            {
                Gizmos.DrawRay(position, direction * distance);
                Gizmos.DrawWireSphere(position, 0.1f);
                break;
            }

            Gizmos.DrawRay(position, direction * (disToHit == 0f ? distance : disToHit));
            position += direction * disToHit;
            Gizmos.DrawWireSphere(position, 0.1f);

            distance -= disToHit;

            if (normal != Vector2.zero)
                direction = Vector3.Reflect(direction, normal);

            
            if (i++ > 100)
                break;
        }
        Debug.Log($"i: {i}");
    }
}
