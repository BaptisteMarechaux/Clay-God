using UnityEngine;
using System.Collections;

public class BattleHPBar : MonoBehaviour {
    [SerializeField]
    BattleEntity entity;
    [SerializeField]
    GameObject hpBar;

    float s;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        s = (float)entity.HP / (float)entity.HPMax;
        hpBar.transform.localScale = Vector3.Lerp(hpBar.transform.localScale, new Vector3(s, hpBar.transform.localScale.y, hpBar.transform.localScale.z), Time.deltaTime);
        
	}
}
