using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialCursor : MonoBehaviour {
    [SerializeField]
    private TutorialManager tutorialManager;
    [SerializeField]
    private InputManager input;
    //Prefabs for Range and Mvt Display
    [SerializeField]
    private GameObject mvtObj;
    [SerializeField]
    private GameObject rangeObj;

    [SerializeField]
    BattleUnit controledUnit;

    [SerializeField]
    BattleUnit enemyUnit;

    [SerializeField]
    BattleGod controledGod;

    [SerializeField]
    BattleGod enemyGod;

    private Vector2 moveCount=new Vector2(0,0);//Verifie combien de mouvements sont effectués
	private Vector2 adVal = new Vector2(0,0);//Check is movement is still possible
    private Vector3 originalPos; //Position d'origine que l'on va garder au cas ou le joueur invalide un déplacement

    [SerializeField]
    Text tutoText;

	// Update is called once per frame
	void Update () {
        InputManagment();
	}

    void InputManagment()
    {
        if (input.Adown)
        {
            switch (tutorialManager.tutorialState)
            {
                case TutorialManager.TutorialState.firstExplainations:

                    break;

                case TutorialManager.TutorialState.firstMove:
                    if (transform.position.x == controledUnit.transform.position.x && transform.position.z == controledUnit.transform.position.z)
                    {

                    }
                    break;

                case TutorialManager.TutorialState.firstAPress:

                    break;
            }
        }

        if (tutorialManager.tutorialState != TutorialManager.TutorialState.selectingAction && tutorialManager.tutorialState != TutorialManager.TutorialState.selectingGodAction && tutorialManager.tutorialState != TutorialManager.TutorialState.firstExplainations)
        {
            if (input.LeftDown)
            {
                adVal.x = -1; adVal.y = 0;
                if (isMovable())
                {
                    if (tutorialManager.tutorialState == TutorialManager.TutorialState.selectingUnit || tutorialManager.tutorialState == TutorialManager.TutorialState.selectingTarget)
                    {
                        moveCount.x--;
                    }

                    gameObject.transform.Translate(Vector3.left);
                }

            }
            if (input.RightDown)
            {
                adVal.x = 1; adVal.y = 0;
                if (isMovable())
                {
                    if (tutorialManager.tutorialState == TutorialManager.TutorialState.selectingUnit || tutorialManager.tutorialState == TutorialManager.TutorialState.selectingTarget)
                        moveCount.x++;

                    gameObject.transform.Translate(Vector3.right);
                }

            }

            if (input.DownDown)
            {
                adVal.x = 0; adVal.y = -1;
                if (isMovable())
                {
                    if (tutorialManager.tutorialState == TutorialManager.TutorialState.selectingUnit || tutorialManager.tutorialState == TutorialManager.TutorialState.selectingTarget)
                        moveCount.y--;

                    gameObject.transform.Translate(Vector3.back);
                }

            }
            if (input.UpDown)
            {
                adVal.x = 0; adVal.y = 1;
                if (isMovable())
                {
                    if (tutorialManager.tutorialState == TutorialManager.TutorialState.selectingUnit || tutorialManager.tutorialState == TutorialManager.TutorialState.selectingTarget)
                        moveCount.y++;

                    gameObject.transform.Translate(Vector3.forward);
                }

            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 500))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    //if (hit.transform == truc) ;
                    //gameObject.transform.position = new Vector3(hit.point.x, gameObject.transform.position.y, hit.point.y);
                }

            }

        }
        
    }

    void moveCountClamp()
	{
		if(moveCount.x < -controledUnit.Movement)
			moveCount.x = -controledUnit.Movement;
		if(moveCount.y < -controledUnit.Movement)
			moveCount.y = -controledUnit.Movement;
		if(moveCount.x > controledUnit.Movement)
			moveCount.x = controledUnit.Movement;
		if(moveCount.y > controledUnit.Movement)
			moveCount.y = controledUnit.Movement;
	}

	bool isMovable() //Vérifie si on peut encore faire avancer le curseur
	{
        switch(tutorialManager.tutorialState)
        {
            case TutorialManager.TutorialState.selectingUnit:
                if ((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > controledUnit.Movement)
                    return false;
                break;

            case TutorialManager.TutorialState.selectingTarget:
                if ((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > controledUnit.Range)
                    return false;
                break;

        }
		return true;
	}

    void OnTriggerEnter()
    {

    }
}
