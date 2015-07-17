using UnityEngine;
using System.Collections;

public class BattleUnitReactionCommand : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    [SerializeField]
    Collider detectionCollider;

    [SerializeField]
    ConfrontationManager confrontationManager;

    BattleUnit detectedUnit;

    [SerializeField]
    InputManager inputManager;

    [SerializeField]
    GameObject buttonToPressObject;

    bool TimeCountStarted;

    float t = 0;
    float maxT;

    [SerializeField]
    int player;

	void Start () {
        maxT = confrontationManager.reactionTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(TimeCountStarted)
        {
            t += Time.deltaTime;
                
            if (inputManager.Adown)
            {
                confrontationManager.StartConfrontation(detectedUnit);
                buttonToPressObject.SetActive(false);
                TimeCountStarted = false;
                t = 0;
            }

            if (t >= maxT)
            {
                TimeCountStarted = false;
                t = 0;
                buttonToPressObject.SetActive(false);
            }
        }
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (player == inputManager.currentPlayer)
        {
            if (battleMain.battleState == BattleMain.Battlestate.enemyTurn)
            {
                detectedUnit = col.GetComponent<BattleUnit>();
                if (detectedUnit.IsEnemy)
                {
                    if (!TimeCountStarted)
                    {
                        buttonToPressObject.SetActive(true);
                        TimeCountStarted = true;
                    }


                }
            }
        }
        
        
    }
}
