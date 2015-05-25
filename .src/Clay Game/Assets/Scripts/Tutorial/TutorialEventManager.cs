using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialEventManager : MonoBehaviour {
    public enum TutorialState //utilisé pour bloquer les scripts d'interaction
    {
        firstExplains,
        cursormoving,
        selectingCharacter,
        selectingAPositionForCharacter,
        selectingAnAction,
        selectingATarget,
        selectingASecondCharacter,
        selectingForWait,
        enemyTurn,
        enemyConfrontPause,
        endOfTutorial
    }

    public TutorialState tutorialState;

    //Classe qui va gérer les évènements du tutoriel
    [SerializeField]
    Image BlackImage;

    [SerializeField]
    GameObject GodShowObject;
    [SerializeField]
    GameObject UnitShowObject;
    [SerializeField]
    GameObject EnemyUnitShowObject;

    [SerializeField]
    GameObject nextButtonObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TextEventStart(int eventIndex)
    {
        switch(eventIndex)
        {
            case 3:
                GodShowObject.SetActive(true);
                break;

            case 5:
                GodShowObject.SetActive(false);
                UnitShowObject.SetActive(true);
                break;

            case 7:
                UnitShowObject.SetActive(false);
                EnemyUnitShowObject.SetActive(true);
                break;

            case 9:
                EnemyUnitShowObject.SetActive(false);
                break;

            case 10:
                BlackImage.gameObject.SetActive(false);
                //nextButtonObject.SetActive(false);
                tutorialState = TutorialState.cursormoving;
                break;

            case 12:
                tutorialState = TutorialState.selectingCharacter;
                break;
            case 15:
                tutorialState = TutorialState.selectingAnAction;
                break;
            case 23:
                ReturnToTitle();
                break;

        }
    }

    public void ReturnToTitle()
    {
        Application.LoadLevel("SceneTitleScreen");
    }
}
