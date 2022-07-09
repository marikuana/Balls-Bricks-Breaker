using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Block : Item
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

    public void Initialize(Vector3 position, float health)
    {
        Initialize(position);
        SetHealth(health);
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void Damage(float value)
    {
        SetHealth(health - value);
        if (!Alive)
            Destroy();
    }

    public bool Alive => 
        health > 0;

    public void SetHealth(float value)
    {
        SetText(value.ToString());
        health = value;
    }

    private void SetText(string text)
    {
        label.text = text.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out BallBase bullet))
            bullet.Impact(this);
    }
}
