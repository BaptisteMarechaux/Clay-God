using UnityEngine;
using System.Collections;

public class BattleActionSelect : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;
    [SerializeField]
    GameObject ActionButtonsGroup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(battleMain.battleState == BattleMain.Battlestate.selectingCharAction)
        {
            ActionButtonsGroup.gameObject.SetActive(true);
        }
	}

    public void SelectAttack()
    {
        battleMain.battleState = BattleMain.Battlestate.selectingAtkTarget;
        ActionButtonsGroup.gameObject.SetActive(false);

    }
    public void SelectWait()
    {
        battleMain.battleState = BattleMain.Battlestate.waiting;
        ActionButtonsGroup.gameObject.SetActive(false);
    }
}
