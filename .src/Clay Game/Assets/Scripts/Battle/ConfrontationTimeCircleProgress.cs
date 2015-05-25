using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfrontationTimeCircleProgress : MonoBehaviour {
    [SerializeField]
    ConfrontationManager confrontManager;

    [SerializeField]
    Image timeImage;

    [SerializeField]
    Text timeText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(confrontManager.timeLeft>=0)
        {
            timeImage.fillAmount = confrontManager.timeLeft / confrontManager.confrontationTime;
            timeText.text = (Mathf.RoundToInt(confrontManager.confrontationTime - confrontManager.timeLeft)).ToString();
        }
	}
}
