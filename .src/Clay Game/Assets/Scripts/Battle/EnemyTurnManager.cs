using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTurnManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    

    BattleUnit selectedEnemy;

    List<Vector3> tempDestinations;
    int a = 0;
    int enemyCount;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        /*
       if(battleMain.battleState == BattleMain.Battlestate.enemyTurn)
       {
           if(selectedEnemy.TurnEnded)
           {
               AttackCheck();
           }
       }
         * */
	}

    void OnEnable()
    {
        enemyCount = battleMain.EnemyEntities.Count;
        for (int i = 0; i < enemyCount;i++ )
        {
            battleMain.EnemyEntities[i].target.GetComponent<BattleEntity>().ChangeHP(-battleMain.EnemyEntities[i].Power);
            battleMain.EnemyEntities[i].TurnEnded = true;
        }
        battleMain.EnemyTurnEnd();
        gameObject.SetActive(false);
            
        //Move();
    }

    void Move()
    {
        
        if (a < battleMain.EnemyEntities.Count)
        {
             if(battleMain.EnemyEntities[a].TurnEnded == false)
             {
                 selectedEnemy = battleMain.EnemyEntities[a];
                 AttackCheck();
                 //selectedEnemy.FindPath();


             }
             
        }  

    }

    void AttackCheck()
    {
        bool okAttack = false;
                for (int i = -selectedEnemy.Range; i <= selectedEnemy.Range; i += 1)
                {
                    for (int j = (Mathf.Abs(i) - selectedEnemy.Range); j <= selectedEnemy.Range - Mathf.Abs(i); j += 1)
                    {
                        //selectedEnemy.targetSelector.transform.position = new Vector3(i, 0, j);
                       /* if(selectedEnemy.target.position.x == (i + transform.position.x) && selectedEnemy.target.position.z == (j+transform.position.z))
                        {
                            okAttack = true;
                        }*/
                        okAttack = true;
                    }

                }
        if(okAttack)
        {
            selectedEnemy.target.GetComponent<BattleUnit>().ChangeHP(-selectedEnemy.Power);
        }
        selectedEnemy.TurnEnded = true;
        battleMain.IsTurnEndedForEnemies();
        a++;
        if(this.gameObject.activeSelf && battleMain.EnemyEntities.Count <= a)
           Move();
    }

    void FindDestination(Vector3 destination)
    {
        tempDestinations = new List<Vector3>();


    }
}
