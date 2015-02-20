using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTurnManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    

    BattleUnit selectedEnemy;

    List<Vector3> tempDestinations;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        if (battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            Move();
        }
	}

    void Move()
    {

        for (int i = 0; i < selectedEnemy.Range; i++)
        {
            for (int j = 0; j < selectedEnemy.Range; j++)
            {
                //Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                //bool AttackPossible = !(Physics.CheckSphere(worldPoint, nodeRadius * 0.9f, unwalkableMask));
            }
        }
    }

    void FindDestination(Vector3 destination)
    {
        tempDestinations = new List<Vector3>();


    }
}
