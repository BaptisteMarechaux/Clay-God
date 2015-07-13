using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {

    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle fullscreenToggle;

    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void VolumeChange()
    {

    }

    public void ResolutionChange(int screenH)
    {
        Screen.SetResolution(Mathf.CeilToInt(screenH*1.7777f), screenH, fullscreenToggle.isOn);
    }

    public void QualitySettingsChange(int value)
    {
        switch(value)
        {
            case 0:
                QualitySettings.SetQualityLevel(0,true);
                break;
            case 1:
                QualitySettings.SetQualityLevel(3, true);
                break;

            case 2:
                QualitySettings.SetQualityLevel(5, true);
                break;

            default :
                QualitySettings.SetQualityLevel(3, true);
                break;
        }
    }

    public void backFromOptions()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

     public void ShowOptions()
    {
        optionsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
}
