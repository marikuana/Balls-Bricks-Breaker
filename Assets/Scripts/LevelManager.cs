using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<Level> levels = new List<Level>();

    public LevelManager()
    {
        levels = Resources.LoadAll<Level>("Levels").ToList();
    }

    public List<Level> GetLevels() => levels;
}
