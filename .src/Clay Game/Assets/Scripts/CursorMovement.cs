using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
	[SerializeField]
	private InputManager input;
	//Prefabs for Range and Mvt Display
	[SerializeField]
	private GameObject mvtObj;
	[SerializeField]
	private GameObject rangeObj;
	public bool canSelect;
	public bool charSelected;
	private bool hover;
	private Transform selectedCharTransform;
	private CharacterStats hoverCharacter;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(input.Adown)
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

		if(input.Bdown)
		{
			if(charSelected)
				charSelected = false;
		}

		if(input.leftDown)
		{
			gameObject.transform.Translate(Vector3.left);
		}
		
		if(input.rightDown)
		{
			gameObject.transform.Translate(Vector3.right);
		}
		
		if(input.downDown)
		{
			gameObject.transform.Translate(Vector3.back);
		}
		if(input.upDown)
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
			hoverCharacter = col.GetComponent<CharacterStats>();
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
				if(hoverCharacter.alreadySelected)
				{
					for(int i=0;i<hoverCharacter.mvtRangeObjects.Count;i++)
					{
						hoverCharacter.mvtRangeObjects[i].SetActive(false);
					}
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
			if(hoverCharacter.alreadySelected)
			{
				for(int i=0;i<hoverCharacter.mvtRangeObjects.Count;i++)
				{
					hoverCharacter.mvtRangeObjects[i].SetActive(true);
				}
			}
			else
			{
				//Movement Display
				for(int i=0;i<hoverCharacter.Movement+1;i++)
				{
					for(int j=-i-1;j<i;j++)
					{
						tmpMvtClone =(GameObject)Instantiate(mvtObj, new Vector3(i-hoverCharacter.Movement, 1, j+1), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
					
				}
				
				for(int i=0;i<hoverCharacter.Movement;i++)
				{
					for(int j=-i-1;j<i;j++)
					{
						tmpMvtClone =(GameObject)Instantiate(mvtObj, new Vector3(hoverCharacter.Movement-i, 1, j+1), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
					
				}


				//Range Display
				//Left
				for(int i=0;i>=-hoverCharacter.Movement;i--)
				{
					for(int j=-hoverCharacter.Movement;j>-(hoverCharacter.Movement+hoverCharacter.Range);j--)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j-1-i), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}

					for(int j=hoverCharacter.Movement;j<hoverCharacter.Movement+hoverCharacter.Range;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j+1+i), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
				}
				for(int i=1;i<hoverCharacter.Range+1;i++)
				{
					Debug.Log(hoverCharacter.Range-i);
					for(int j=0;j<=hoverCharacter.Range-i;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(-hoverCharacter.Movement-i, 1, -j), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
						Debug.Log(tmpMvtClone.transform.position);
					}


					for(int j=1;j<=hoverCharacter.Range-i;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(-hoverCharacter.Movement-i, 1, j), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
				}
				//Right
				for(int i=1;i<=hoverCharacter.Movement;i++)
				{
					for(int j=-hoverCharacter.Movement;j>-(hoverCharacter.Movement+hoverCharacter.Range);j--)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j-1+i), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
					
					for(int j=hoverCharacter.Movement;j<hoverCharacter.Movement+hoverCharacter.Range;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j+1-i), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
				}
				for(int i=1;i<hoverCharacter.Range+1;i++)
				{
					Debug.Log(hoverCharacter.Range-i);
					for(int j=0;j<=hoverCharacter.Range-i;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(hoverCharacter.Movement+i, 1, -j), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
						Debug.Log(tmpMvtClone.transform.position);
					}
					
					
					for(int j=1;j<=hoverCharacter.Range-i;j++)
					{
						tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(hoverCharacter.Movement+i, 1, j), Quaternion.identity);
						tmpMvtClone.transform.parent = hoverCharacter.transform;
						hoverCharacter.mvtRangeObjects.Add(tmpMvtClone);
					}
				}

				hoverCharacter.alreadySelected = true;
			}

		}
	}

}
