using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {
    [SerializeField]
    GameObject ElementsEcranChargement;

    [SerializeField]
    GameObject CameraEcranChargement;

	// Use this for initialization
	void Start () {
        Application.LoadLevelAdditive(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            
            ElementsEcranChargement.SetActive(false);
            CameraEcranChargement.SetActive(false);
           
        }

    }
}
