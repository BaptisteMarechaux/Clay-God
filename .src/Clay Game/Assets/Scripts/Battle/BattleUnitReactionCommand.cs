using UnityEngine;
using System.Collections;

public class BattleUnitReactionCommand : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    [SerializeField]
    Collider detectionCollider;
	// Use this for initialization

    BattleUnit detectedUnit;
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            detectedUnit = col.GetComponent<BattleUnit>();
            if(detectedUnit.IsEnemy)
            {

            }
        }
        
    }
}
