using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] 
    public float health = 10f;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TextMeshPro label;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void Start()
    {
        SetText(health.ToString());
    }

    public Block Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        return this;
    }

    public void Damage(float value)
    {
        SetHealth(health - value);
        if (!Alive)
            Destroy();
    }

    public bool Alive => 
        health > 0;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetHealth(float value)
    {
        SetText(value.ToString());
        health = value;
    }

    private void SetText(string text)
    {
        label.text = text.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out BallBase bullet))
            bullet.Impact(this);
    }
}
