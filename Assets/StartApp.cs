using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartApp : MonoBehaviour
{
    [SerializeField]
    private Text loadText;
    private int pointCount;

    private void Start()
    {
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
