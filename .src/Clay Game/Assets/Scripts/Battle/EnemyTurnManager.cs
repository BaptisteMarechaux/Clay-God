using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTurnManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    

    BattleUnit selectedEnemy;

    List<Vector3> tempDestinations;
    int a = 0; //Simplement l'index de l'ennemi qui effectue des actions
    int enemyCount;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        
       if(battleMain.battleState == BattleMain.Battlestate.enemyTurn)
       {
           if(selectedEnemy.TurnEnded)
           {
               AttackCheck();
           }
       }
        
	}

    void OnEnable()
    {
        enemyCount = battleMain.EnemyEntities.Count;
        a = 0;
        Move();
    }

    void Move()
    {
        
        if (a < battleMain.EnemyEntities.Count)
        {
             if(battleMain.EnemyEntities[a].TurnEnded == false)
             {
                 if (!battleMain.EnemyEntities[a].isActiveAndEnabled)
                 {
                     a++;
                     Move();
                 }
                     
                 selectedEnemy = battleMain.EnemyEntities[a];
                 //AttackCheck();
                 selectedEnemy.FindPath();


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
                        if (selectedEnemy.target.position.x == (i + selectedEnemy.transform.position.x) && selectedEnemy.target.position.z == (j + selectedEnemy.transform.position.z))
                        {
                            //Debug.Log("trouvé !");
                            okAttack = true;
                        }
                        
                        //okAttack = true;
                    }

                }
        if(okAttack)
        {
            selectedEnemy.target.GetComponent<BattleEntity>().ChangeHP(-selectedEnemy.Power);
            okAttack = false;
        }
        //selectedEnemy.TurnEnded = true;
        battleMain.IsTurnEndedForEnemies();
        a++;
        if(this.gameObject.activeSelf && battleMain.EnemyEntities.Count > a)
           Move();
    }

    void FindDestination(Vector3 destination)
    {
        tempDestinations = new List<Vector3>();


    }
}
