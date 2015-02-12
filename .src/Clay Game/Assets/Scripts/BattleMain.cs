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
        hoverSideKick
	};

	public Battlestate battleState;

	[SerializeField]
	GameObject mobileButtons;

    [SerializeField]
    List<BattleEntity> PlayerEntities;

    [SerializeField]
    List<BattleEntity> EnemyEntities;
	// Use this for initialization
	void Start () {
        /*
		if (Application.platform == RuntimePlatform.Android)
			mobileButtons.SetActive(true);
		else
			mobileButtons.SetActive(false);
        */
		battleState = BattleMain.Battlestate.waiting;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(battleState);
	}

    public void IsTurnEndedForAll()
    {
        bool over=true; //vrai si le tour doit etre fini , faux sinon
        for(int i=0;i<PlayerEntities.Count;i++)
        {
            //Verifie si chaque entité du joueur a deja joué son tour
            if (!PlayerEntities[i].TurnEnded)
                over = false;
        }

        if(over)
        {
            Debug.Log("LE TOUR EST TERMINE");
            for (int i = 0; i < PlayerEntities.Count; i++)
            {
                PlayerEntities[i].TurnEnded = false;
            }
        }
    }
}
