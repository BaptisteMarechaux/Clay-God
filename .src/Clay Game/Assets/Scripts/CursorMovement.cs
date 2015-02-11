using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorMovement : MonoBehaviour {
	[SerializeField]
	private BattleMain battleMain;
	[SerializeField]
	private InputManager input;
	//Prefabs for Range and Mvt Display
	[SerializeField]
	private GameObject mvtObj;
	[SerializeField]
	private GameObject rangeObj;
	private bool canSelect; // On peut sélectionner une entité
	private bool charSelected; //On a déja sélectionné une entité
	private bool hover; //On survole une entité
	private Transform selectedCharTransform; //Transform de l'entité selectionnée
	private BattleUnit hoverCharacter; //Entité qui est survolée par le curseur et enventuellement, celle qui est sélectionnée
	private BattleEntity selectedTarget; //Quand on choisit une cible, c'est la cible en cours quand on a pas encore choisi
	private Vector2 moveCount=new Vector2(0,0);//Verifie combien de mouvements sont effectués
	private Vector2 adVal = new Vector2(0,0);//Check is movement is still possible
    private Vector3 originalPos; //Position d'origine que l'on va garder au cas ou le joueur invalide un déplacement
    [SerializeField]
    GameObject ActionButtonsGroup;//Référence le groupe d'UI ou vont apparaitre les bouton d'attaque et d'attente

    bool hurting;
    Color c;
    int ci = 0;

    [SerializeField]
    Text importantText;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        InputManagment();

	}

    void InputManagment()
    {
        if (input.Adown)
        {
            switch(battleMain.battleState)
            {
                case BattleMain.Battlestate.selectingAtkTarget:
                    selectedTarget.ChangeHP(-hoverCharacter.Power);
                     hoverCharacter.TurnEnded = true;
                     //hoverCharacter.HidePanels();
                     hoverCharacter.HideRangeForAttacking();
                     battleMain.battleState = BattleMain.Battlestate.waiting;
                    break;

                case BattleMain.Battlestate.selectingCharAction:
                    break;

                case BattleMain.Battlestate.selectingCharacter:
                    CharacterMoveValidation();
                    ActionButtonsGroup.gameObject.SetActive(true);
                    battleMain.battleState = BattleMain.Battlestate.selectingCharAction;
                    break;

                case BattleMain.Battlestate.hoverCharacter:
                    battleMain.battleState = BattleMain.Battlestate.selectingCharacter;
                    break;

            }     
        }

        if (input.Bdown)
        {
            Canceling();
        }

        
        if(battleMain.battleState != BattleMain.Battlestate.selectingCharAction)
        {
            if (input.LeftDown)
            {
                adVal.x = -1; adVal.y = 0;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
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
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                        moveCount.x++;

                    gameObject.transform.Translate(Vector3.right);
                }

            }

            if (input.DownDown)
            {
                adVal.x = 0; adVal.y = -1;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                        moveCount.y--;

                    gameObject.transform.Translate(Vector3.back);
                }

            }
            if (input.UpDown)
            {
                adVal.x = 0; adVal.y = 1;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                        moveCount.y++;

                    gameObject.transform.Translate(Vector3.forward);
                }

            }
        }
       
    }
	
	void moveCountClamp()
	{
		if(moveCount.x < -hoverCharacter.Movement)
			moveCount.x = -hoverCharacter.Movement;
		if(moveCount.y < -hoverCharacter.Movement)
			moveCount.y = -hoverCharacter.Movement;
		if(moveCount.x > hoverCharacter.Movement)
			moveCount.x = hoverCharacter.Movement;
		if(moveCount.y > hoverCharacter.Movement)
			moveCount.y = hoverCharacter.Movement;
	}

	bool isMovable() //Vérifie si on peut encore faire avancer le curseur
	{
		if((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > hoverCharacter.Movement)
			return false;
		return true;
	}

    bool isMovableForSelectAttack()
    {
        //Fonction qui va servir a vérifier si on peut déplacer le curseur quand on séléctionne une cible
        if ((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > hoverCharacter.Range)
            return false;
        return true;
    }

	bool CharacterSelection()
	{
		if(canSelect)
		{
			return true;
		}
		return false;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Character")
		{
			if(battleMain.battleState == BattleMain.Battlestate.waiting)
			{
				hoverCharacter = col.GetComponent<BattleUnit>();
                selectedCharTransform = col.transform;
                if(!hoverCharacter.TurnEnded)
                {
                    battleMain.battleState = BattleMain.Battlestate.hoverCharacter;
                    RangeDisplay();
                   
                }
			}

           // if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)

			if(battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
			{
				selectedTarget = col.GetComponent<BattleEntity>();
			}

		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Character"){
			if(battleMain.battleState == BattleMain.Battlestate.hoverCharacter)
			{
				if(hoverCharacter.AlreadySelected)
				{
					hoverCharacter.HidePanels();
				}
				hover = false;
				battleMain.battleState = BattleMain.Battlestate.waiting;
			}

		}

	}

	void RangeDisplay()
	{
		if(battleMain.battleState == BattleMain.Battlestate.hoverCharacter)
		{
			if(hoverCharacter.AlreadySelected)
			{
				hoverCharacter.ShowPanels();
			}
			else
			{
				hoverCharacter.CreateRangePanels(rangeObj);
				hoverCharacter.CreateMovementPanels(mvtObj);
			}

		}
	}

    void CharacterMoveValidation()
    {
        originalPos = selectedCharTransform.position;
        selectedCharTransform.position = new Vector3(gameObject.transform.position.x, selectedCharTransform.position.y, gameObject.transform.position.z);
        hoverCharacter.HidePanels();
        hoverCharacter.ShowRangeForAttacking();
        adVal.x = 0; adVal.y = 0;
        moveCount.x = 0; moveCount.y = 0;
    }
	
	void Canceling()
	{
		if(battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
		{
            gameObject.transform.position= new Vector3(selectedCharTransform.position.x, gameObject.transform.position.y, selectedCharTransform.position.z);
            battleMain.battleState = BattleMain.Battlestate.hoverCharacter;
		}

		if(battleMain.battleState == BattleMain.Battlestate.selectingCharAction)
		{
            selectedCharTransform.position = originalPos;
            hoverCharacter.ShowPanels();
            hoverCharacter.HideRangeForAttacking();
			battleMain.battleState = BattleMain.Battlestate.selectingCharacter;
		}

		if(battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
		{
			battleMain.battleState = BattleMain.Battlestate.selectingCharAction;
		}
	}

    public void SelectAttack()
    {
        battleMain.battleState = BattleMain.Battlestate.selectingAtkTarget;
        ActionButtonsGroup.gameObject.SetActive(false);

    }
    public void SelectWait()
    {
        //Fonction décrivant ce qu'il se passe quand une unité finit son tour sans rien faire
        hoverCharacter.TurnEnded = true;
        hoverCharacter.HidePanels();
        battleMain.battleState = BattleMain.Battlestate.waiting;
        ActionButtonsGroup.gameObject.SetActive(false);
    }

    public void SetTextInfo()
    {
        if(battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
        {
            if (selectedTarget != null)
                importantText.text = selectedTarget.Nickname + selectedTarget.HP + "/" + selectedTarget.HPMax;
        }
        else
        {
            if(hoverCharacter != null)
                importantText.text = hoverCharacter.Nickname + hoverCharacter.HP + "/" + hoverCharacter.HPMax;
        }
    }

    void FixedUpdate()
    {
        SetTextInfo();
    }
}
