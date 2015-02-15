using UnityEngine;
using System.Collections;

public class Node{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool isWalkable, Vector3 worldPos, int GridX, int GridY)
    {
        walkable = isWalkable;
        worldPosition = worldPos;
        gridX = GridX;
        gridY = GridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
