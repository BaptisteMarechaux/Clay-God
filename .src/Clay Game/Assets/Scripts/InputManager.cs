using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public bool leftDown
	{
		get{return Input.GetKeyDown(KeyCode.LeftArrow);}
	}

	public bool rightDown
	{
		get{return Input.GetKeyDown(KeyCode.RightArrow);}
	}

	public bool downDown
	{
		get{return Input.GetKeyDown(KeyCode.DownArrow);}
	}

	public bool upDown
	{
		get{return Input.GetKeyDown(KeyCode.UpArrow);}
	}

	public bool Adown
	{
		get{return Input.GetKeyDown(KeyCode.X);}
	}

	public bool Bdown
	{
		get{return Input.GetKeyDown(KeyCode.C);}
	}

	public bool Xdown
	{
		get{return Input.GetKeyDown(KeyCode.V);}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
