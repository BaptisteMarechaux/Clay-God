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

    bool TimeCountStarted;

    float t = 0;
    float maxT;
	void Start () {
        maxT = confrontationManager.reactionTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Battle State " + battleMain.battleState);
        if(TimeCountStarted)
        {
            t += Time.deltaTime;
                
            if (inputManager.Adown)
            {
                confrontationManager.StartConfrontation();
            }

            if (t >= maxT)
            {
                TimeCountStarted = false;
                t = 0;
            }
        }
	    
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        Debug.Log(battleMain.battleState);
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
