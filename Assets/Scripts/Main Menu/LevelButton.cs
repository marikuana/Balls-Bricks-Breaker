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

    private Level level;

    private void Awake()
    {
        select.onClick.AddListener(SelectLevel);
    }

    public void Init(int levelNumber, Level level)
    {
        SetNumber(levelNumber);
        stars.SetStars(Manager.Instance.ProgressData.GetLevelStar(level));
        this.level = level;
    }

    private void SelectLevel()
    {
        LoadScreen.Instance.Load(new GameSceneLoader(level));
    }

    public void SetNumber(int number)
    {
        label.text = number.ToString("00");
    }
}
