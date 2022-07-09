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
    public BaseBlockFactory BlockFactory;
    [SerializeField]
    public ItemFactory ItemFactory;
    [SerializeField]
    public List<LevelBlocks> Blocks;
    [SerializeField]
    public BallType[] balls;
    [SerializeField]
    public List<MoneyItems> MoneyItems;

    private void Awake()
    {
        if (string.IsNullOrEmpty(LevelId))
            LevelId = Guid.NewGuid().ToString();
    }
}
