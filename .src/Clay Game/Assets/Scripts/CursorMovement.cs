using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
	[SerializeField]
	private BattleMain battleMain;

	[SerializeField]
	private InputManager input;
	//Prefabs for Range and Mvt Display
	[SerializeField]
	private GameObject mvtObj;
	[SerializeField]
	private GameObject rangeObj;
	private bool canSelect;
	private bool charSelected;
	private bool hover;
	private Transform selectedCharTransform;
	private BattleUnit hoverCharacter;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(input.Adown)
		{
			CharacterMove();
		}

		if(input.Bdown)
		{
			Canceling();
		}

		if(input.LeftDown)
		{
			gameObject.transform.Translate(Vector3.left);
		}
		
		if(input.RightDown)
		{
			gameObject.transform.Translate(Vector3.right);
		}
		
		if(input.DownDown)
		{
			gameObject.transform.Translate(Vector3.back);
		}
		if(input.UpDown)
		{
			gameObject.transform.Translate(Vector3.forward);
		}



	}

	void FixedUpdate()
	{
		RangeDisplay();
	}

	bool CharacterSelection()
	{
		if(canSelect)
		{
			return true;
		}
			

		return false;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Character")
		{
			hoverCharacter = col.GetComponent<BattleUnit>();
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Character")
		{
			hover = true;
			RangeDisplay();
			selectedCharTransform = col.transform;
			if(!charSelected)
				canSelect = true;
		}
	}
	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Character"){
			if(!charSelected)
			{
				if(hoverCharacter.AlreadySelected)
				{
					hoverCharacter.HidePanels();
				}
				hover = false;
			}
			else
			{
				canSelect = false;
			}
		}
	}

	void RangeDisplay()
	{
		GameObject tmpMvtClone;
		if(hover)
		{
			if(hoverCharacter.AlreadySelected)
			{
				hoverCharacter.ShowPanels();
			}
			else
			{
				hoverCharacter.CreateRangePanels(rangeObj);
				hoverCharacter.CreateMovementPanels(mvtObj);
			}

		}
	}

	void CharacterMove()
	{
		if(charSelected)
			{
				selectedCharTransform.position = new Vector3(gameObject.transform.position.x, selectedCharTransform.position.y,gameObject.transform.position.z);
				charSelected = false;
			}

			if(CharacterSelection() && !charSelected)
			{
				charSelected = true;
			}
	}
	
	void Canceling()
	{
		if(battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
		{
			if(charSelected)
				charSelected = false;
		}

		if(battleMain.battleState == BattleMain.Battlestate.selectingCharAction)
		{
			battleMain.battleState = BattleMain.Battlestate.selectingCharacter;
		}

		if(battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
		{
			battleMain.battleState = BattleMain.Battlestate.selectingCharAction;
		}
	}
}
