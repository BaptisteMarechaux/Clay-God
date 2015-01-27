using UnityEngine;
using System.Collections;

public class BattleMain : MonoBehaviour {
	public enum Battlestate{


	}

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
