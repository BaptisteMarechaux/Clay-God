using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleMain : MonoBehaviour {
	public enum Battlestate{
		waiting,
		hoverCharacter,
		selectingCharacter,
		selectingCharAction,
		selectingAtkTarget,
		enemyTurn,
        hoverGod,
        hoverSideKick,
        placingUnit
	};

	public Battlestate battleState;

	[SerializeField]
	GameObject mobileButtons;

    public List<BattleUnit> PlayerEntities;

    public List<BattleUnit> EnemyEntities;

    public List<BattleGod> PlayerGodEntities;

    public List<BattleGod> EnemyGodEntities;

    public List<BattleSideKick> PlayerSideKickEntities;

    public List<BattleSideKick> EnemySideKickEntities;

    public EnemyTurnManager enemyTurnManager;

    public GameObject pTurn, eTurn, Vic, Defe;

	// Use this for initialization
	void Start () {
        
		if (Application.platform == RuntimePlatform.Android)
			mobileButtons.SetActive(true);
		else
			mobileButtons.SetActive(false);
        
		battleState = BattleMain.Battlestate.waiting;
        
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(battleState);
	}

    public void IsTurnEndedForAll() //Verifie si le tour de chaque unité est terminé puis lance la phase ennemie en conséquence
    {
        bool over=true; //vrai si le tour doit etre fini , faux sinon
        for(int i=0;i<PlayerEntities.Count;i++)
        {
            //Verifie si chaque entité du joueur a deja joué son tour
            if (!PlayerEntities[i].TurnEnded && PlayerEntities[i].gameObject.activeSelf)
                over = false;
        }

        if(over)
        {
            Debug.Log("LE TOUR EST TERMINE");
            eTurn.SetActive(true);
            
            battleState = BattleMain.Battlestate.enemyTurn;
            for (int i = 0; i < EnemyEntities.Count;i++ )
            {
                EnemyEntities[i].target = PlayerEntities[Random.Range(0, PlayerEntities.Count - 1)].transform;
            }
            enemyTurnManager.enabled = true;

        }
    }

    public void IsTurnEndedForEnemies() //Verifie si le tour de chaque unité est terminé puis lance la phase ennemie en conséquence
    {
        bool over = true; //vrai si le tour doit etre fini , faux sinon
        for (int i = 0; i < EnemyEntities.Count; i++)
        {
            //Verifie si chaque entité du joueur a deja joué son tour
            if (!EnemyEntities[i].TurnEnded && EnemyEntities[i].gameObject.activeSelf)
                over = false;
        }

        if (over)
        {
            Debug.Log("LE TOUR ENNEMI EST TERMINE");
           pTurn.SetActive(true);
           for (int i = 0; i < PlayerEntities.Count; i++)
           {
               PlayerEntities[i].TurnEnded = true;
           }
            battleState = BattleMain.Battlestate.waiting;
            enemyTurnManager.enabled = false;

        }
    }

    public void Victory()
    {
        Vic.SetActive(true);
    }

    public void Defeat()
    {
        Defe.SetActive(true);
    }
}
