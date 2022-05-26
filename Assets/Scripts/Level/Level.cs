using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    [SerializeField]
    public string LevelId;
    [SerializeField]
    public Block Pref;
    [SerializeField]
    public List<LevelObjects> objects;
    [SerializeField]
    public BallType[] balls;

    private void Awake()
    {
        LevelId = Guid.NewGuid().ToString();
    }
}
