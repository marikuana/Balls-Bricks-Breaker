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

        while (distance > 0f)
        {
            Vector2 normal = Vector2.zero;
            float disToHit = 0f;

            RaycastHit2D hit = Physics2D.CircleCast(position + direction * 0.01f, 0.1f, direction, distance);
            if (hit != default(RaycastHit2D))
            {
                disToHit = hit.distance;
                normal = hit.normal;
                if (hit.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(1);
                }

                SetColor(Random.value > 0.5f ? Color.yellow : Color.blue);
            }
            else
            {
                disToHit = distance;
            }

            position += direction * disToHit;

            distance -= disToHit;

            if (normal != Vector2.zero)
                direction = Vector3.Reflect(direction, normal).normalized;
        }

        transform.position = position;
        movement = direction;

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

    private void OnDrawGizmosSelected()
    {
        int i = 0;
        Gizmos.color = Color.yellow;

        Vector3 position = transform.position;
        float distance = gizmosDistance;
        Vector3 direction = movement.normalized;

        while (distance > 0f)
        {
            Vector2 normal = Vector2.zero;
            float disToHit;

            position = position + direction * 0.1f;
            Gizmos.DrawWireSphere(position, 0.1f);
            RaycastHit2D hit = Physics2D.CircleCast(position, 0.1f, direction, distance);
            if (hit != default(RaycastHit2D))
            {
                disToHit = hit.distance;
                normal = hit.normal;
            }
            else
            {
                disToHit = distance;
            }

            Gizmos.DrawRay(position, direction * disToHit);
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
