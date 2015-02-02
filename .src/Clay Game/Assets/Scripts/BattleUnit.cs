using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleUnit : BattleEntity {
	/// Classe décrivant une unité, ses statistiques et certaines de ses actions
	[SerializeField]
	private int power; //Puissance de l'unité
	public int Power{
		get{return power;}
	}
	[SerializeField]
	private int resist; //Résistance de l'unité
	[SerializeField]
	private int movement;
	public int Movement{
		get{return movement;}
	}

	[SerializeField]
	private int range;
	public int Range{
		get{return range;}
	}

	[HideInInspector]
	public bool panelsCreated;
	private bool moving;

	private bool alreadySelected;
	public bool AlreadySelected{
		get{return alreadySelected;}
		set{alreadySelected = value;}
	}

	private List<GameObject> mvtPanels = new List<GameObject>();
	public List<GameObject> MvtPanels{
		get{return mvtPanels;}
	}

	private List<GameObject> rangePanels = new List<GameObject>();
	public List<GameObject> RangePanels{
		get{ return rangePanels;}
	}
	
	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MovementPanelReset()
	{
		panelsCreated=false;
	}
	
	void RangePanelReset()
	{
		panelsCreated=false;
	}
	
	public void PanelReset()
	{
		MovementPanelReset();
		
		RangePanelReset ();
	}

	public void CreateMovementPanels(GameObject mvtObj)
	{
		GameObject tmpMvtClone;
		//Movement Display
		for(int i=0;i<Movement+1;i++)
		{
			for(int j=-i-1;j<i;j++)
			{
				tmpMvtClone =(GameObject)Instantiate(mvtObj, new Vector3(i-Movement, 1, j+1), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(i-Movement, - 1, j+1);
				mvtPanels.Add(tmpMvtClone);
			}
			
		}
		
		for(int i=0;i<Movement;i++)
		{
			for(int j=-i-1;j<i;j++)
			{
				tmpMvtClone =(GameObject)Instantiate(mvtObj, new Vector3(Movement-i, 1, j+1), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(Movement-i, - 1, j+1);
				mvtPanels.Add(tmpMvtClone);
			}
			
		}
		alreadySelected = true;
	}

	public void CreateRangePanels(GameObject rangeObj)
	{
		GameObject tmpMvtClone;
		//Range Display
		//Left
		for(int i=0;i>=-Movement;i--)
		{
			for(int j=-Movement;j>-(Movement+Range);j--)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j-1-i), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(i, - 1, j-1-i);
				rangePanels.Add(tmpMvtClone);
			}
			
			for(int j=Movement;j<Movement+Range;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j+1+i), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(i, - 1, j+1+i);
				rangePanels.Add(tmpMvtClone);
			}
		}
		for(int i=1;i<Range+1;i++)
		{
			Debug.Log(Range-i);
			for(int j=0;j<=Range-i;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(-Movement-i, 1, -j), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(-Movement-i, - 1, -j);
				rangePanels.Add(tmpMvtClone);
			}
			
			
			for(int j=1;j<=Range-i;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(-Movement-i, 1, j), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(-Movement-i,- 1, j);
				rangePanels.Add(tmpMvtClone);
			}
		}
		//Right
		for(int i=1;i<=Movement;i++)
		{
			for(int j=-Movement;j>-(Movement+Range);j--)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j-1+i), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(i,- 1, j-1+i);
				rangePanels.Add(tmpMvtClone);
			}
			
			for(int j=Movement;j<Movement+Range;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(i, 1, j+1-i), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(i,- 1, j+1-i);
				rangePanels.Add(tmpMvtClone);
			}
		}
		for(int i=1;i<Range+1;i++)
		{
			for(int j=0;j<=Range-i;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(Movement+i, 1, -j), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(Movement+i, - 1, -j);
				rangePanels.Add(tmpMvtClone);
			}
			
			
			for(int j=1;j<=Range-i;j++)
			{
				tmpMvtClone = (GameObject)Instantiate(rangeObj, new Vector3(Movement+i, 1, j), Quaternion.identity);
				tmpMvtClone.transform.parent = transform;
				tmpMvtClone.transform.localPosition = new Vector3(Movement+i, - 1, j);
				rangePanels.Add(tmpMvtClone);
			}
		}
		alreadySelected = true;
	}

	public void ShowPanels()
	{
		for(int i=0;i<rangePanels.Count;i++)
		{
			rangePanels[i].gameObject.SetActive(true);
		}
		for(int i=0;i<mvtPanels.Count;i++)
		{
			mvtPanels[i].gameObject.SetActive(true);
		}
	}

	public void HidePanels()
	{
		for(int i=0;i<rangePanels.Count;i++)
		{
			rangePanels[i].gameObject.SetActive(false);
		}
		for(int i=0;i<mvtPanels.Count;i++)
		{
			mvtPanels[i].gameObject.SetActive(false);
		}
	}


	
	public void Move(Vector3 destination)
	{
		if(moving)
		{
			gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, destination, Time.deltaTime);
		}
	}
}
