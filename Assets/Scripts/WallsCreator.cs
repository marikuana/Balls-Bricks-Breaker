using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPref;
    [SerializeField]
    private Vector3 position = Vector3.zero;
    [SerializeField]
    private Vector3 sizeOffset = Vector3.zero;

    private void Awake()
    {
        CreateWalls(GetWallsTransform(position));
    }

    private Vector3 GetSizeWalls()
    {
        Camera cam = Camera.main;
        return cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, 0)) + sizeOffset;
    }

    private List<Tuple<Vector3, Vector3>> GetWallsTransform(Vector3 position)
    {
        Vector3 size = GetSizeWalls();

        return new List<Tuple<Vector3, Vector3>>
        {
            new Tuple<Vector3, Vector3>(position + new Vector3(size.x, 0, 0), new Vector3(0.1f, size.y * 2f, 0)),
            new Tuple<Vector3, Vector3>(position + new Vector3(-size.x, 0, 0), new Vector3(0.1f, size.y * 2f, 0)),
            new Tuple<Vector3, Vector3>(position + new Vector3(0, size.y, 0), new Vector3(size.x * 2f, 0.1f, 0))
        };
    }

    private void CreateWalls(List<Tuple<Vector3, Vector3>> transforms)
    {
        foreach (var item in transforms)
            CreateWall(item.Item1, item.Item2);
    }

    private void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = Instantiate(wallPref, transform);
        wall.transform.position = position;
        wall.transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        List<Tuple<Vector3, Vector3>> transforms = GetWallsTransform(position);

        Gizmos.color = Color.green;
        foreach (var item in transforms)
            Gizmos.DrawCube(item.Item1, item.Item2);
    }
}
