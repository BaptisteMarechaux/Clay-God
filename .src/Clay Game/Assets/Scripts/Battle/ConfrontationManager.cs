using UnityEngine;
using System.Collections;

public class ConfrontationManager : MonoBehaviour {
    GameObject ConfrontationImageGroup;

    [SerializeField]
    BattleMain battleMain;

    public float reactionTime; //Temps qu'il faut pour avoir le temps de déclencher la confrontation

    [SerializeField]
    int confrontationTime;
    int timeLeft;

    public enum ConfrontationType
    {
        SingleButton,
        MultipleButtons,
        SingleButtonReaction
    }

    public enum ConfrontationCommands
    {
        A, //Bouton A, Space, entree ou autre validation
        B, //Buton B, Echap, ou autre annulation
        X, //Bouton Alternatif
        Y, //Bouton Alternatif
        Up,
        Down,
        Left,
        Right,

    }

    [SerializeField]
    ConfrontationType confrontationType;

    [SerializeField]
    ConfrontationCommands[] commandsToType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartConfrontation()
    {
        ConfrontationImageGroup.SetActive(true);
    }
}
