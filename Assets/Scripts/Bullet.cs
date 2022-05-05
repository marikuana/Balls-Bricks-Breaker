using System;
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

            RaycastHit2D hit = Physics2D.CircleCast(position + direction * 0.1f, 0.1f, direction, distance);
            if (hit != default(RaycastHit2D))
            {
                RaycastHit2D[] hits = GetHits(position, 0.1f);
                if (hits.Length > 0)
                {
                    normal = Average(hits.Select(s => s.normal).ToArray());

                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                        damageable.Damage(1);

                    SetColor(UnityEngine.Random.value > 0.5f ? Color.yellow : Color.blue);
                }
            }


            if (normal != Vector2.zero)
            {
                Vector3 newDirection = Vector3.Reflect(direction, normal);
                if (newDirection == direction)
                    direction = Average(direction, normal);
                else if (newDirection == Vector3.zero)
                    direction *= -1;
                else
                    direction = newDirection;
                direction = direction.normalized;
            }

            Vector3 velocity = direction * speed * Time.deltaTime;
            position += velocity;// disToHit;

            distance -= velocity.magnitude;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="radius"></param>
    /// <returns>normal</returns>
    public RaycastHit2D[] GetHits(Vector2 position, float radius)
    {
        List<Vector2> vectors = new List<Vector2>()
        {
            new Vector2(0, 1),
            new Vector2(0.5f, 0.5f),
            new Vector2(1, 0),
            new Vector2(0.5f, -0.5f),
            new Vector2(0, -1),
            new Vector2(-0.5f, -0.5f),
            new Vector2(-1, 0),
            new Vector2(-0.5f, 0.5f)
        };
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        foreach (var vector in vectors)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, vector, radius);
            if (hit != default(RaycastHit2D))
                hits.Add(hit);
        }
        return hits.ToArray();
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
            float hitDis = 0f;

            RaycastHit2D hit = Physics2D.CircleCast(position + direction * 0.1f, 0.1f, direction, distance);
            if (hit != default(RaycastHit2D))
            {
                hitDis = hit.distance + 0.1f;
                RaycastHit2D[] hits = GetHits(position + direction * hitDis, 0.1f);
                if (hits.Length > 0)
                {
                    normal = Average(hits.Select(s => s.normal).ToArray());

                    foreach (var hit2D in hits)
                        DrawSphere(hit2D.point, Color.red);
                }
            }

            Gizmos.DrawRay(position, direction * hitDis);
            DrawSphere(position + direction * hitDis, Color.yellow);

            if (normal != Vector2.zero)
            {
                Vector3 newDirection = Vector3.Reflect(direction, normal);
                if (newDirection == direction)
                    direction = Average(direction, normal);
                else if (newDirection == Vector3.zero)
                    direction *= -1;
                else
                    direction = newDirection;
                direction = direction.normalized;
                log.AppendLine($"newDirection => {direction}");
            }

            Vector3 velocity = direction * hitDis;
            
            position += velocity;// disToHit;

            distance -= velocity.magnitude;
            
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
