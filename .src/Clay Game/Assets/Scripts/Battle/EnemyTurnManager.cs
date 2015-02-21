using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTurnManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    

    BattleUnit selectedEnemy;

    List<Vector3> tempDestinations;
    int a = 0;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    void OnEnable()
    {
        Move();
    }

    void Move()
    {
        Debug.Log(battleMain.EnemyEntities[a].TurnEnded);
        if (a < battleMain.EnemyEntities.Count)
        {
             if(battleMain.EnemyEntities[a].TurnEnded == false)
             {
                 selectedEnemy = battleMain.EnemyEntities[a];
                 selectedEnemy.FindPath();


             }
             
        }  
                //Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                //bool AttackPossible = !(Physics.CheckSphere(worldPoint, nodeRadius * 0.9f, unwalkableMask));

    }

    void AttackCheck()
    {
       
                for (int i = -selectedEnemy.Range; i <= selectedEnemy.Range; i += 1)
                {
                    for (int j = (Mathf.Abs(i) - selectedEnemy.Range); j <= selectedEnemy.Range - Mathf.Abs(i); j += 1)
                    {
                        selectedEnemy.targetSelector.transform.position = new Vector3(i, 0, j);
                    }

                }
                selectedEnemy.targetSelector.transform.position = selectedEnemy.transform.position;
                selectedEnemy.ChangeHP(-selectedEnemy.Power);
                battleMain.EnemyEntities[a].TurnEnded = true;
                a++;
    }

    void FindDestination(Vector3 destination)
    {
        tempDestinations = new List<Vector3>();


    }
}
