using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelDescription : MonoBehaviour {
    [SerializeField]
    Text LevelTitle;

    [SerializeField]
    Text LevelDes;

    [SerializeField]
    List<string> Titles;

    [SerializeField]
    List<string> Descriptions;


	// Use this for initialization
	void Start () {
        LevelTitle.text = "Rocky Plain";
        LevelDes.text = "A place where many wars were declared";
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ChangeLevelInfos(int level)
    {
        LevelTitle.text = Titles[level];
        LevelDes.text = Descriptions[level];

    }
}
