using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartApp : MonoBehaviour
{
    [SerializeField]
    private Manager manager;

    [SerializeField]
    private TextMeshProUGUI loadText;
    private int pointCount;

    private void Awake()
    {
        DontDestroyOnLoad(manager.gameObject);
        SceneManager.LoadScene("Main Menu");
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        while (true)
        {
            StringBuilder text = new StringBuilder("Loading");
            text.Append('.', ++pointCount);
            loadText.text = text.ToString();

            if (pointCount >= 3)
                pointCount = 0;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
