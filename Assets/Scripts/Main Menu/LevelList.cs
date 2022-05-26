using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelList : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Button close;
    [SerializeField]
    private LevelButton pref;
    [SerializeField]
    private Transform content;

    private void Awake()
    {
        close.onClick.AddListener(OnClickClose);

        float size = Screen.width / 9.6f;
        content.GetComponent<GridLayoutGroup>().cellSize = new Vector2 (size, size);
    }

    private void Start()
    {
        List<Level> levels = Manager.Instance.LevelManager.GetLevels();

        for (int i = 1; i <= levels.Count; i++)
        {
            LevelButton levelButton = Instantiate(pref, content);
            levelButton.gameObject.SetActive(true);
            levelButton.Init(i, levels[i - 1]);
        }
    }

    public void OnClickOpen()
    {
        canvas.enabled = true;
    }

    public void OnClickClose()
    {
        canvas.enabled = false;
    }
}

