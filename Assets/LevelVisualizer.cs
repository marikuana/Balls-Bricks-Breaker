using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVisualizer : MonoBehaviour
{
    [SerializeField]
    private Level level;

    private void OnDrawGizmos()
    {
        if (level == null)
            return;

        Gizmos.color = Color.yellow;

        foreach (var block in level.Blocks)
        {
            Gizmos.DrawWireCube(block.Position, Vector3.one);
        }
    }
}
