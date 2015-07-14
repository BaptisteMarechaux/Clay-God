using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleMenuManager : MonoBehaviour {
    [SerializeField]
    GameObject battleMenuCanvas;
    [SerializeField]
    GameObject itemMenuCanvas;
    [SerializeField]
    GameObject unitListMenuCanvas;
    [SerializeField]
    GameObject mapOverviewCanvas;
    [SerializeField]
    GameObject mapOverviewCamera;

    [SerializeField]
    Text menuButtonText;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowBattleMenu()
    {
        battleMenuCanvas.SetActive(!battleMenuCanvas.activeSelf);
        menuButtonText.text = battleMenuCanvas.activeSelf?"Close":"Menu";
        itemMenuCanvas.SetActive(false);
        unitListMenuCanvas.SetActive(false);
        mapOverviewCanvas.SetActive(false);
        mapOverviewCamera.SetActive(false);
    }

    public void HideItemMenu()
    {
        if (battleMenuCanvas.activeSelf)
        {
            battleMenuCanvas.SetActive(false);
        }
    }

    public void ShowItemMenu()
    {
        itemMenuCanvas.SetActive(true);
        battleMenuCanvas.SetActive(false);
        menuButtonText.text = "Menu";
    }

    public void ShowUnitListMenu()
    {
        unitListMenuCanvas.SetActive(true);
        battleMenuCanvas.SetActive(false);
        menuButtonText.text = "Menu";
    }

    public void ShowMapOverviewMenu()
    {
        mapOverviewCanvas.SetActive(true);
        battleMenuCanvas.SetActive(false);
        mapOverviewCamera.SetActive(true);
        menuButtonText.text = "Menu";
    }

    public void HideMapOverview()
    {
        mapOverviewCanvas.SetActive(false);
        mapOverviewCamera.SetActive(false);
    }

    public void ForcedTurnEnd()
    {
        battleMenuCanvas.SetActive(false);
        menuButtonText.text = "Menu";
    }

}
