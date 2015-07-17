using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	public Transform target;
	[SerializeField]
	private float speed = 10;
    [SerializeField]
    BattleMain battleMain;
    [SerializeField]
    float targetOffset=3;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 30, Space.Self);
        }
        else if (d < 0f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 30, Space.Self);
        }
	}

	void FixedUpdate()
	{
       
		transform.position = Vector3.Lerp(transform.position, TargetPosition(), speed*Time.deltaTime);
	}

	Vector3 TargetPosition()
	{
        if (battleMain.battleState == BattleMain.Battlestate.enemyTurn)
        {
            return new Vector3(target.position.x, transform.position.y, target.position.z - targetOffset);
        }
		return new Vector3(target.position.x, transform.position.y, target.position.z -targetOffset);

	}
}
