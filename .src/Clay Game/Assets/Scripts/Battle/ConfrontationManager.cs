using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfrontationManager : MonoBehaviour {
    [SerializeField]
    GameObject ConfrontationImageGroup;

    [SerializeField]
    BattleMain battleMain;

    public float reactionTime; //Temps qu'il faut pour avoir le temps de déclencher la confrontation

    [SerializeField]
    float confrontationTime;
    float timeLeft;

    float enemyGaugeLevel;
    float playerGaugeLevel;

    [SerializeField]
    Image playerGaugeImage;
    [SerializeField]
    Image enemyGaugeImage;

    public float enemySpeed;

    [SerializeField]
    InputManager inputManager;

 

    public enum ConfrontationType
    {
        SingleButton,
        MultipleButtons,
        SingleButtonReaction
    }

    public enum ConfrontationCommands
    {
        A, //Bouton A, Space, entree ou autre validation
        B, //Buton B, Echap, ou autre annulation
        X, //Bouton Alternatif
        Y, //Bouton Alternatif
        Up,
        Down,
        Left,
        Right,

    }

    [SerializeField]
    ConfrontationType confrontationType;

    [SerializeField]
    ConfrontationCommands[] commandsToType;

	// Use this for initialization
	void Start () {
        playerGaugeImage.fillAmount = 0.5f;
        enemyGaugeImage.fillAmount = 0.5f;

        //StartConfrontation();
	}
	
	// Update is called once per frame
	void Update () {
        if(battleMain.battleState == BattleMain.Battlestate.confrontationActive)
        {
            if (timeLeft < confrontationTime)
            {

                ExectuteConfrontationCommand();
                timeLeft += Time.deltaTime;
            }
            else
            {
                EndConfrontation();
            }
        }
        
        
	}

    public void StartConfrontation()
    {
        battleMain.battleState = BattleMain.Battlestate.confrontationActive;
        timeLeft = 0;
        ConfrontationImageGroup.SetActive(true);
        playerGaugeLevel = 0.5f;
        enemyGaugeLevel = 0.5f;
    }
    
    public void EndConfrontation()
    {
        ConfrontationImageGroup.SetActive(false);
        battleMain.battleState = BattleMain.Battlestate.enemyTurn;
    }

    void ExectuteConfrontationCommand()
    {
        enemyGaugeLevel += Time.deltaTime * 0.1f;
        if (inputManager.Adown)
        {
            playerGaugeLevel += 0.05f;
        }
        /*
        if (playerGaugeLevel > 1) playerGaugeLevel = 1;
        if (playerGaugeLevel < 0) playerGaugeLevel = 0;
        if(enemyGaugeLevel > 1) enemyGaugeLevel = 1;
        if (enemyGaugeLevel < 0) enemyGaugeLevel = 0;
        */


        playerGaugeImage.fillAmount = playerGaugeLevel/(playerGaugeLevel+enemyGaugeLevel);
        enemyGaugeImage.fillAmount = enemyGaugeLevel / (playerGaugeLevel + enemyGaugeLevel);
    }

    bool CheckCommandToType()
    {
        return false;
    }
}
