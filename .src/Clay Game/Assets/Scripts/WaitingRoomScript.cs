using UnityEngine;
using System.Collections;

public class WaitingRoomScript : MonoBehaviour {
    [SerializeField]
    GameObject ElementsEcranChargement;

    [SerializeField]
    GameObject CameraEcranChargement;

	// Use this for initialization
	void Start () {
        Application.LoadLevelAdditive("TacticalMovementTestScene");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 3)
        {
            
            ElementsEcranChargement.SetActive(false);
            CameraEcranChargement.SetActive(false);

        }

    }
}
