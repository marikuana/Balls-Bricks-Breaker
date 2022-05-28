using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    private List<Level> levels = new List<Level>();

    public LevelManager()
    {
        levels = Resources.LoadAll<Level>("Levels").ToList();
    }

    public List<Level> GetLevels() => levels;

    public Level GetNextLevel(Level level)
    {
        int index = levels.FindIndex(f => f.LevelId == level.LevelId);
        if (index + 1 >= levels.Count || index == -1)
            return levels[index];
        return levels[index + 1];
    }
}
