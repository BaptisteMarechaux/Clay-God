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

    [SerializeField]
    bool isSoloPlay;
    public bool IsSoloPlay
    {
        get{return isSoloPlay;}
    }

    public AudioSource[] audioSources;
 

	public Battlestate battleState;

	[SerializeField]
	GameObject mobileButtons;

    [SerializeField]
    CameraScript battleCamera;


    public List<CursorMovement> cursors;

    //Unités du joueur 1
    public List<BattleUnit> PlayerEntities;

    //Unités du joueur 2
    public List<BattleUnit> Player2Entities;

    public List<BattleUnit> EnemyEntities;

    //Gods des joueurs
    public List<BattleGod> PlayerGodEntities;

    public List<BattleGod> EnemyGodEntities;

    public List<BattleSideKick> PlayerSideKickEntities;

    public List<BattleSideKick> EnemySideKickEntities;

    public EnemyTurnManager enemyTurnManager;

    public GameObject pTurn, eTurn, Vic, Defe;

    public Grid grid;

    public InputManager input;

    public Vector3[] clientPositions = new Vector3[5];

    public SaveScript save;

	// Use this for initialization

    void Awake()
    {
        if (!isSoloPlay)
        {
            if (Network.isServer)
            {
                input.currentPlayer = 1;
            }
            else
            {
                Network.Connect(NetworkManager.GameToJoin);
                var index = NetworkManager.GameToJoin.connectedPlayers;
                Debug.Log("index : " + index);

                input.currentPlayer = 2;
            }
        }
        else
        {
            input.currentPlayer = 1;
        }
        
    }

    void OnConnectedToServer()
    {
        int index = 1;
        if(NetworkManager.GameToJoin != null)
        {
            index = NetworkManager.GameToJoin.connectedPlayers + 1;
            Debug.Log("index : " + index);
        }



    }
	void Start () {
        
        /*
		if (Application.platform == RuntimePlatform.Android)
			mobileButtons.SetActive(true);
		else
			mobileButtons.SetActive(false);
        */
		battleState = BattleMain.Battlestate.waiting;

        battleCamera.target.position = new Vector3(PlayerGodEntities[input.currentPlayer-1].transform.position.x, battleCamera.target.position.y, PlayerGodEntities[input.currentPlayer - 1].transform.position.z);

        if (Network.isServer)
        {
            for (int i = 0; i < PlayerEntities.Count; i++)
            {
                PlayerEntities[i].Power = Random.Range(4, 11);
                PlayerEntities[i].Resist = Random.Range(0, 3);
            }

            for (int i = 0; i < Player2Entities.Count; i++)
            {
                Player2Entities[i].Power = Random.Range(4, 11);
                Player2Entities[i].Resist = Random.Range(0, 3);
            }

            for (int i = 0; i < EnemyEntities.Count; i++)
            {
                EnemyEntities[i].Power = Random.Range(3, 7);
                EnemyEntities[i].Resist = Random.Range(0, 3);
            }
        }
	}
	
	void Update () {
		Debug.Log(battleState);
        
	}

    void FixedUpdate()
    {
        if (Network.isServer)
        {
            for (int i = 0; i < 5; i++)
            {
                Player2Entities[i].transform.position = clientPositions[i];
            }
        }
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
        if (!isSoloPlay)
        {
            for (int i = 0; i < Player2Entities.Count; i++)
            {
                if (!Player2Entities[i].TurnEnded && Player2Entities[i].gameObject.activeSelf)
                    over = false;
            }

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
                EnemyEntities[i].target = PlayerGodEntities[Random.Range(0,PlayerGodEntities.Count)].transform;
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
            pTurn.SetActive(true);
            for (int i = 0; i < PlayerEntities.Count; i++)
            {
                PlayerEntities[i].TurnEnded = false;
            }
            if (!isSoloPlay)
            {
                for (int i = 0; i < Player2Entities.Count; i++)
                {
                    Player2Entities[i].TurnEnded = false;
                }
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
        pTurn.SetActive(true);
        for (int i = 0; i < PlayerEntities.Count; i++)
        {
            PlayerEntities[i].TurnEnded = false;
        }
        if (!isSoloPlay)
        {
            for (int i = 0; i < Player2Entities.Count; i++)
            {
                Player2Entities[i].TurnEnded = false;
            }
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
        
        save.PlayerInfo.Money += 1000;
        if (isSoloPlay)
        {
            Application.LoadLevel("SceneSelectLevelMountain");
        }
        else
        {
            Vic.SetActive(true);
        }
    }

    public void Defeat()
    {
        Defe.SetActive(true);
    }

    public void ForceTurnEnd()
    {
        eTurn.SetActive(true);
        audioSources[0].gameObject.SetActive(false);
        audioSources[1].gameObject.SetActive(true);
        battleState = BattleMain.Battlestate.enemyTurn;

        for (int i = 0; i < EnemyEntities.Count; i++)
        {
            //EnemyEntities[i].target = PlayerEntities[Random.Range(0, PlayerEntities.Count - 1)].transform;
            EnemyEntities[i].target = PlayerGodEntities[0].transform;
        }

        enemyTurnManager.enabled = true;
    }

    public void GoBackToTitle()
    {
        Network.Disconnect();
        if(Network.isServer)
            MasterServer.UnregisterHost();
        Application.LoadLevel(0);
    }
}
