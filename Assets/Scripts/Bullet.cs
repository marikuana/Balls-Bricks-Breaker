using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public Vector3 movement;
    private Vector3 spawnPosition;
    [SerializeField] private float destroyDistance = 10f;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
        if (Vector3.Distance(spawnPosition, transform.position) > destroyDistance)
        {
            Gun.Balls--;
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            damageable.Damage(1);
        
        Vector3 normal = collision.contacts.First().normal;
        movement = Vector3.Reflect(movement, normal);

        SetColor(Random.value > 0.5f ? Color.yellow : Color.blue);
    }

    private void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
