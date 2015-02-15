using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTurnManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    

    GameObject selectedEnemy;

    List<Vector3> tempDestinations;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        if (battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            
        }
	}

    void Move()
    {

    }

    void FindDestination(Vector3 destination)
    {
        tempDestinations = new List<Vector3>();


    }
}
