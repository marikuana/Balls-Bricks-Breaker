using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Block _block;
    private List<Block> _blocks = new List<Block>();

    public ProgressData ProgressData;
    private void Awake()
    {
        _blocks = new List<Block>();
        ProgressData = new ProgressData();
    }

    private void Start()
    {
        RespawnBrick();
    }

    /*void Start()
    {
        Vector3 startPos = new Vector3(-5, 3, 0);
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                Block block = Instantiate(_block);
                block.transform.position = new Vector3(startPos.x + i * (block.transform.localScale.x), startPos.y - j * (block.transform.localScale.y), 0);
                block.health = (i == 0 || j == 0 || i == 24 || j == 14) ? 500 : (float)Random.Range(10, 100);
                block.SetColor((i + j) % 2 == 0 ? Color.white : Color.gray);
            }
        }    
    }*/

    public void RespawnBrick()
    {
        DestroyBricks();

        for (int i = 0; i < 5; i++)
        {
            SpawnBrick(Vector3.one * i, UnityEngine.Random.Range(10, 20));
        }
    }

    private void SpawnBrick(Vector3 position, int health)
    {
        Block block = Instantiate(_block);
        block.transform.position = position;
        block.health = health;

        _blocks.Add(block);
    }

    private void DestroyBricks()
    {
        foreach (var block in _blocks)
            block.Destroy();
        _blocks.Clear();
    }
}
