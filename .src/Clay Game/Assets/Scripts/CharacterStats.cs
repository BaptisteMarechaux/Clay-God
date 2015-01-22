using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour {
	[SerializeField]
	private string charaname;
	[SerializeField]
	private int movement;
	[SerializeField]
	private int range;
	[SerializeField]
	private bool canBeSelected; //Only Personal Allies can be selected
	public bool alreadySelected;
	[HideInInspector]
	public List<GameObject> mvtRangeObjects;

	public int Movement{
		get{return movement;}
	}

	public int Range{
		get{return range;}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
