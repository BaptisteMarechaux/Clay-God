using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialTextManager : MonoBehaviour {
    [SerializeField]
    Text infoText;

    [SerializeField]
    TutorialEventManager eventManager;


    [SerializeField]
    List<string> textLines;

    public int currentLine = 0;

    [SerializeField]
    float duration;
    bool disap; //Définit si le texte est en train de disparaitre ou non
    float t; //time

	// Use this for initialization
	void Start () {
        infoText.text = textLines[0];
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void ShowNextLine()
    {
        if ((currentLine+1) < textLines.Count)
        {
            currentLine++;
            infoText.text = textLines[currentLine];
            eventManager.TextEventStart(currentLine);
        }
    }
}
