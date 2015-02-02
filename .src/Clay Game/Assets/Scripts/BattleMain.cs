using UnityEngine;
using System.Collections;

public class BattleMain : MonoBehaviour {
	public enum Battlestate{
		waiting,
		hoverCharacter,
		selectingCharacter,
		selectingCharAction,
		selectingAtkTarget,
		enemyTurn
	};

	public Battlestate battleState;

	[SerializeField]
	GameObject mobileButtons;
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
}
