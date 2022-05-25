using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Stars : MonoBehaviour
{
    [SerializeField]
    private Image[] stars;
    
    public void SetStars(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Color color = stars[i].color;
            color.a = i >= count ? 0.5f : 1f;
            stars[i].color = color;
        }
    }
}
