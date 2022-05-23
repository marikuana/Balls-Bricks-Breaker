using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Controller controller;


    public void RegenerateBricks()
    {
    }

    public void Pause()
    {
        controller.TogglePause();
    }
}
