using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCounter : MonoBehaviour
{
    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    void Update()
    {
        SetBulletCount(Launcher.Balls);    
    }

    private void SetBulletCount(int value)
    {
        label.text = $"Bullets: {value}";
    }
}
