using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour, IDamageable
{
    [SerializeField] public float health = 10f;
    private SpriteRenderer spriteRenderer;
    private TextMesh label;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        label = GetComponentInChildren<TextMesh>();
    }

    private void Start()
    {
        SetText(health.ToString());
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
        label.characterSize = 0.2f - 0.04f * text.Length;
        label.text = text.ToString();
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
