using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyFindPath : MonoBehaviour {
    Grid grid;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FindTarget(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathSucces = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        while(!pathSucces)
        {
            if (startNode == targetNode)
            {
                pathSucces = true;
                break;
            }

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            while(openSet.Count > 0)
            {
                Node currentNode = openSet[0];
            }
                
        }
    }

    void FindPath(Vector3 startPos, Vector3 endPos)
    {


    }
}
