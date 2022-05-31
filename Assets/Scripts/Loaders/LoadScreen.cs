using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    public static LoadScreen Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI loadLabel;
    private Canvas canvas;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        canvas = GetComponent<Canvas>();
        Instance = this;
    }

    public async void Load(params ILoader[] loaders)
    {
        canvas.enabled = true;
        StartCoroutine(Loading());

        foreach (var loader in loaders)
            await loader.Load();

        canvas.enabled = false;
        StopCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        int pointCount = 0;
        while (true)
        {
            StringBuilder text = new StringBuilder("Loading");
            text.Append('.', ++pointCount);
            loadLabel.text = text.ToString();

            if (pointCount >= 3)
                pointCount = 0;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
