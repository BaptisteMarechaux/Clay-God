using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    GameObject connectionButtonGroup;

    [SerializeField]
    GameObject normalButtonGroup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void ShowConnectionButtons()
    {
        connectionButtonGroup.SetActive(true);
        normalButtonGroup.SetActive(false);
    }

    public void HideConnectionButtons()
    {
        if (connectionButtonGroup.activeSelf)
        {
            connectionButtonGroup.SetActive(false);
            normalButtonGroup.SetActive(true);
        }
            
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void LaunchTutorial()
    {
        Application.LoadLevel("TutorialScene");
    }

   
}
