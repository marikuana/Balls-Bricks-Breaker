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
    }

    private void Start()
    {
        List<Level> levels = Resources.LoadAll<Level>("Level").ToList();

        for (int i = 1; i < 22; i++)
        {
            LevelButton levelButton = Instantiate(pref, content);
            levelButton.gameObject.SetActive(true);
            levelButton.Init(i, Random.Range(0, 3));
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

