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
    private Stars stars;

    [SerializeField]
    private TextMeshProUGUI label;

    private void Awake()
    {
        select.onClick.AddListener(SelectLevel);
    }

    public void Init(int levelNumber, int starCount)
    {
        SetNumber(levelNumber);
        stars.SetStars(starCount);
    }

    private void SelectLevel()
    {
        Debug.Log("Select level");
    }

    public void SetNumber(int number)
    {
        label.text = number.ToString("00");
    }
}
