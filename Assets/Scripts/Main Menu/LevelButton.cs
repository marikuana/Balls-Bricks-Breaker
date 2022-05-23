using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private Button select;
    [SerializeField]
    private Image[] stars;
    [SerializeField]
    private TextMeshProUGUI label;

    private void Awake()
    {
        select.onClick.AddListener(SelectLevel);
    }

    public void Init(int levelNumber, int starCount)
    {
        SetNumber(levelNumber);
        SetStar(starCount);
    }

    private void SelectLevel()
    {
        Debug.Log("Select level");
    }

    public void SetStar(int count)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Color color = stars[i].color;
            color.a = i < count ? 1f : 0.5f;
            stars[i].color = color;
        }
    }

    public void SetNumber(int number)
    {
        label.text = number.ToString("00");
    }
}
