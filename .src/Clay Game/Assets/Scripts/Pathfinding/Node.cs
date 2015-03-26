using UnityEngine;
using System.Collections;

public class Node{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost; //Cout par rapport à la grille pour atteindre un noeud suivant
    public int hCost; //Cout par rapport à une heuristique pour atteindre une cible
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
        //Renvoie le cout final : le cout réel par rapport à la grille G plus le cout avec l'heuristique H
        get
        {
            return gCost + hCost;
        }
    }
}
