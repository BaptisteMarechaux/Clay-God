using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	[SerializeField]
	private Transform target;
	[SerializeField]
	private float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, TargetPosition(), speed*Time.deltaTime);
	}

	Vector3 TargetPosition()
	{
		return new Vector3(target.position.x, transform.position.y, target.position.z-3);

	}
}
