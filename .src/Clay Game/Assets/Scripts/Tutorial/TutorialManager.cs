using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {
    public enum TutorialState
    {
        //Etats du jeu pendant le tutoriel
        firstExplainations, //Explications sur le jeu non interactives
        firstMove, //Attendre que le joueur se place pour la première fois sur une unité
        firstAPress, //Attendre que le joueur appuye sur A
        comingToFirstEnemy, //Attendre que le joueur se déplace vers l'ennemi qui lui fait face
        firstSelectAtkTarget,
        enemyTurn,
        selectingUnit,
        selectingGodAction,
        selectingAction,
        selectingTarget,

    };

    public TutorialState tutorialState;

    [SerializeField]
    public List<BattleUnit> PlayerEntities;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
