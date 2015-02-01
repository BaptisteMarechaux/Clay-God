using UnityEngine;
using System.Collections;

public class BattleMain : MonoBehaviour {
	public enum Battlestate{
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
