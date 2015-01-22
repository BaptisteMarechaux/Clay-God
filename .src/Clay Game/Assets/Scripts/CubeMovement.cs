using UnityEngine;
using System.Collections;

public class CubeMovement : MonoBehaviour, IMove {
	public Transform trans;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			trans.Translate(Vector3.forward * Time.deltaTime);
		}
	}

	public void Move()
	{

	}
	
}
