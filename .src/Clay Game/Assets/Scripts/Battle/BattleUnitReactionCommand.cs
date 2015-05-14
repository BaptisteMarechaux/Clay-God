using UnityEngine;
using System.Collections;

public class BattleUnitReactionCommand : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    [SerializeField]
    Collider detectionCollider;

    ConfrontationManager confrontationManager;

    BattleUnit detectedUnit;

    InputManager inputManager;

    bool TimeCountStarted;

    float t = 0;
    float maxT;
	void Start () {
        maxT = confrontationManager.reactionTime;
	}
	
	// Update is called once per frame
	void Update () {
        if(TimeCountStarted)
        {
            t += Time.deltaTime;
            if (t >= maxT)
            {
                TimeCountStarted = false;
                t = 0;
            }
                
            if (inputManager.Adown)
            {
                confrontationManager.StartConfrontation();
            }
        }
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if(battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            detectedUnit = col.GetComponent<BattleUnit>();
            if(detectedUnit.IsEnemy)
            {
                if(!TimeCountStarted)
                    TimeCountStarted = true;
            }
        }
        
    }
}
