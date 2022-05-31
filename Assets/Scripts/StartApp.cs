using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartApp : MonoBehaviour
{  
    private void Start()
    {
        LoadScreen.Instance.Load(new ManagerLoad(), new MainMenuLoad(), new FakeLoad(1000));
    }
}
