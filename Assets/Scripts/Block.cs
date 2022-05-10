using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour, IDamageable
{
    [SerializeField] public float health = 10f;
    private SpriteRenderer spriteRenderer;
    private TextMesh label;
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        label = GetComponentInChildren<TextMesh>();
    }

    private void Start()
    {
        SetText(health.ToString());
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void Damage(int value)
    {
        SetHealth(health - value);
        if (!Alive)
            Destroy();
    }

    public bool Alive => 
        health > 0;

    private void SetHealth(float value)
    {
        SetText(value.ToString());
        health = value;
    }

    private void SetText(string text)
    {
        float scale = 1.2f - (text.Length * 0.2f);
        label.transform.localScale = new Vector3(scale, scale, 0f); ;
        label.text = text.ToString();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Bullet bullet))
            bullet.Impact(this);
    }
}
