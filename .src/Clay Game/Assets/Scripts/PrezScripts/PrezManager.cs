using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class PrezManager : MonoBehaviour {
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    BlurOptimized blurScript;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {

        }
	}

    
}
