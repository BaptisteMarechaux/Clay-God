using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Transform target;
	[SerializeField]
	private float speed = 10;
    [SerializeField]
    BattleMain battleMain;

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
        if (battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            return new Vector3(target.position.x, transform.position.y, target.position.z - 3);
        }
		return new Vector3(target.position.x, transform.position.y, target.position.z-3);

	}
}
