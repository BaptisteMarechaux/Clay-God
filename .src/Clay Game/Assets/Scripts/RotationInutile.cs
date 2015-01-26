using UnityEngine;
using System.Collections;

public class RotationInutile : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		transform.Rotate(100*Vector3.up*Time.deltaTime);
	}
}
