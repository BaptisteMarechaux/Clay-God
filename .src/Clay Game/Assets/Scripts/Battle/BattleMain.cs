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
        selectingGodAction,
        hoverSideKick,
        selectingSideKickAction,
        confrontationActive
	};

    public AudioSource[] audioSources;
 

	public Battlestate battleState;

	[SerializeField]
	GameObject mobileButtons;

    [SerializeField]
    CameraScript battleCamera;

    [SerializeField]
    CameraScript[] battleCameras;

    public List<CursorMovement> cursors;

    public List<BattleUnit> PlayerEntities;

    public List<BattleUnit> EnemyEntities;

    public List<BattleGod> PlayerGodEntities;

    public List<BattleGod> EnemyGodEntities;

    public List<BattleSideKick> PlayerSideKickEntities;

    public List<BattleSideKick> EnemySideKickEntities;

    public EnemyTurnManager enemyTurnManager;

    public GameObject pTurn, eTurn, Vic, Defe;

    public Grid grid;

	// Use this for initialization

    void Awake()
    {
        if (Network.isServer)
        {
            
        }
        else
        {
            Network.Connect(NetworkManager.GameToJoin);
        }
    }

    void OnConnectedToServer()
    {
        int index = 1;
        if(NetworkManager.GameToJoin != null)
        {
            index = NetworkManager.GameToJoin.connectedPlayers;
            Debug.Log(index);
        }
        //battleCamera = battleCameras[0];
        //battleCamera.target = cursors[index].transform;
    }
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
            eTurn.SetActive(true);

            audioSources[0].gameObject.SetActive(false);
            audioSources[1].gameObject.SetActive(true);
            battleState = BattleMain.Battlestate.enemyTurn;
           
            for (int i = 0; i < EnemyEntities.Count;i++ )
            {
                //EnemyEntities[i].target = PlayerEntities[Random.Range(0, PlayerEntities.Count - 1)].transform;
                EnemyEntities[i].target = PlayerGodEntities[0].transform;
            }
             
            enemyTurnManager.enabled = true;

        }
    }

    public void IsTurnEndedForEnemies() //Verifie si le tour de chaque unité est terminé puis lance la phase ennemie en conséquence
    {
        bool over=false; //vrai si le tour doit etre fini , faux sinon
        for (int i = 0; i < EnemyEntities.Count; i++)
        {
            //Verifie si chaque entité du joueur a deja joué son tour
            if (EnemyEntities[i].TurnEnded && EnemyEntities[i].gameObject.activeSelf)
            {
                over = true;
            }
            else
            {
                over = false;
            }
                
        }

        if (over)
        {
            Debug.Log("LE TOUR ENNEMI EST TERMINE");
            pTurn.SetActive(true);
            for (int i = 0; i < PlayerEntities.Count; i++)
            {
                PlayerEntities[i].TurnEnded = false;
            }
            for (int i = 0; i < EnemyEntities.Count;i++ )
            {
                EnemyEntities[i].TurnEnded = false;
            }
            battleState = BattleMain.Battlestate.waiting;
            audioSources[0].gameObject.SetActive(true);
            audioSources[1].gameObject.SetActive(false);
            enemyTurnManager.enabled = false;

        }
    }

    public void EnemyTurnEnd()
    {
        Debug.Log("LE TOUR ENNEMI EST TERMINE");
        pTurn.SetActive(true);
        for (int i = 0; i < PlayerEntities.Count; i++)
        {
            PlayerEntities[i].TurnEnded = false;
        }
        for (int i = 0; i < EnemyEntities.Count; i++)
        {
            EnemyEntities[i].TurnEnded = false;
        }
        battleState = BattleMain.Battlestate.waiting;
        enemyTurnManager.enabled = false;
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
