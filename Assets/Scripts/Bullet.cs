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

    void Awake()
    {
        spawnPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 position = transform.position;
        float distance = speed * Time.deltaTime;
        Vector3 direction = movement.normalized;

        int i = 0;

        while (distance > 0f)
        {
            Vector2 normal = Vector2.zero;
            float disToHit;

            RaycastHit2D hit = Physics2D.CircleCast(position + direction * 0.1f, 0.1f, direction);

            if (hit != default(RaycastHit2D))
            {
                disToHit = hit.distance;
                normal = hit.normal;

                Vector3 contactPoint = position + direction * disToHit + direction * 0.1f;
                RaycastHit2D hit2 = Physics2D.Raycast(contactPoint, Average(Vector2.Reflect(direction, normal), direction), 0.1f);
                if (hit2 != default(RaycastHit2D))
                {
                    normal = Average(normal, hit2.normal);

                    if (hit2.collider.TryGetComponent(out IDamageable damageable1))
                    {
                        damageable1.Damage(1);
                    }
                }

                if(hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(1);
                }

                SetColor(Random.value > 0.5f ? Color.yellow : Color.blue);

            }
            else
            {
                disToHit = distance;
            }

            position += direction * disToHit + direction * 0.1f;

            distance -= disToHit;

            if (normal != Vector2.zero)
            {
                Vector3 newDirection = Vector3.Reflect(direction, normal);
                if (newDirection == direction)
                    direction = Average(direction, normal);
                else if (newDirection == Vector3.zero)
                    direction *= -1;
                else
                    direction = newDirection;

            }

            if (++i >= 100)
            {
                SetColor(Color.black);
                Debug.LogError(null);
            }
        }

        transform.position = position;
        movement = direction.normalized;

        if (Vector3.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Destroy();
        }
    }


    public void ChangeMovement(Vector3 normal)
    {
        SetMovement(Vector3.Reflect(movement, normal.normalized));
    }

    public void SetMovement(Vector3 movement)
    {
        this.movement = movement.normalized;
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

    [SerializeField] private float gizmosDistance = 5f;
    [SerializeField] private int gizmosMaxContact = 1;

    private void OnDrawGizmosSelected()
    {
        StringBuilder log = new StringBuilder();
        int i = 0;
        Gizmos.color = Color.yellow;

        Vector3 position = transform.position;
        float distance = gizmosDistance;
        Vector3 direction = movement.normalized;


        while (distance > 0f)
        {
            Vector2 normal = Vector2.zero;
            float disToHit;

            RaycastHit2D hit = Physics2D.CircleCast(position + direction * 0.1f, 0.1f, direction);

            if (hit != default(RaycastHit2D))
            {
                disToHit = hit.distance;
                normal = hit.normal;
                Debug.Log($"{normal} | {hit.collider.transform.position}");
                Vector3 contactPoint = position + direction * disToHit + direction * 0.1f;
                RaycastHit2D hit2 = Physics2D.Raycast(contactPoint, Average(Vector2.Reflect(direction, normal), direction), 0.1f);
                if (hit2 != default(RaycastHit2D))
                {
                    normal = Average(normal, hit2.normal);
                    DrawSphere(hit2.point, Color.red);
                }

               DrawSphere(hit.point, Color.red);
            }
            else
            {
                disToHit = distance;
            }

            Gizmos.DrawRay(position, direction * disToHit + direction * 0.1f);
            position += direction * disToHit + direction * 0.1f;
            Gizmos.DrawWireSphere(position, 0.1f);

            distance -= disToHit;

            if (normal != Vector2.zero)
            {
                Vector3 newDirection = Vector3.Reflect(direction, normal);
                if (newDirection == direction)
                    direction = Average(direction, normal);
                else if (newDirection == Vector3.zero)
                    direction *= -1;
                else
                    direction = newDirection;

                log.AppendLine($"newDirection => {direction}");
            }

            if (++i >= gizmosMaxContact)
                break;
        }
        log.AppendLine($"i: {i}");
        Debug.Log(log);
    }

    private void DrawSphere(Vector3 position, Color color)
    {
        Color tmp = Gizmos.color;
        Gizmos.color = color;
        Gizmos.DrawWireSphere(position, 0.1f);
        Gizmos.color = tmp;
    }

    private Vector2 Average(params Vector2[] vectors)
    {
        float x = 0f;
        float y = 0f;
        foreach (var v in vectors)
        {
            x += v.x;
            y += v.y;
        }
        x /= vectors.Count();
        y /= vectors.Count();
        return new Vector2(x, y).normalized;
    }
}
