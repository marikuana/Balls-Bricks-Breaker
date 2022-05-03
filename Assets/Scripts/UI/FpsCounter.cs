using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FpsCounter : MonoBehaviour
{
    private Text label;
    private List<float> lastFps;

    private void Awake()
    {
        label = GetComponent<Text>();    
        lastFps = new List<float>();
    }

    private void Start()
    {
        StartCoroutine(FpsUpdate());
    }

    void Update()
    {
        lastFps.Add(1 / Time.deltaTime);
    }

    private IEnumerator FpsUpdate()
    {
        yield return new WaitForSeconds(0.2f);

        float fps = lastFps.Average();
        SetFps(fps);
        lastFps.Clear();

        yield return FpsUpdate();
    }

    private void SetFps(float fps)
    {
        label.text = $"FPS: {fps:0}";
    }
}
