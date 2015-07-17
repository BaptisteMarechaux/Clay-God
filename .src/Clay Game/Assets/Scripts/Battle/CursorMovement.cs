using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CursorMovement : MonoBehaviour {
    //Script qui va gérer tout le mouvement du curseur
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
    [SerializeField]
	private BattleUnit hoverCharacter; //Entité qui est survolée par le curseur et enventuellement, celle qui est sélectionnée
    private BattleGod hoverGod; //Dieu qui est survolé par le curseur et plus tard sélectionné  
    private BattleSideKick hoverSideKick;
	private BattleEntity selectedTarget; //Quand on choisit une cible, c'est la cible en cours quand on a pas encore choisi
    private Collider selectedCollider; //Collider qu'on touche quand on sélectionne une entité pour une attaque
    private Collider hoverCollider; //Collider que l'on touche quand on survole une entité
	private Vector2 moveCount=new Vector2(0,0);//Verifie combien de mouvements sont effectués
	private Vector2 adVal = new Vector2(0,0);//Check is movement is still possible
    private Vector3 originalPos; //Position d'origine que l'on va garder au cas ou le joueur invalide un déplacement
    [SerializeField]
    GameObject ActionButtonsGroup;//Référence le groupe d'UI ou vont apparaitre les bouton d'attaque et d'attente
    [SerializeField]
    GameObject GodActionButtonsGroup;
    [SerializeField]
    GameObject SideKickActionButtonsGroup;
    private bool isImpossiblePosition; //Variable qui va servir a vérifier si on essaye de placer une unité sur un obstacle 
    bool hurting;
    Color c;
    int ci = 0;

    [SerializeField]
    Text hoverEntityNameText; // Texte des informations sur l'unité survolée
    [SerializeField]
    Text hoverEntityHPText;
    [SerializeField]
    Text hoverEntityPowerText;
    [SerializeField]
    Text hoverEntityResistText;
    [SerializeField]
    Image hoverEntityHPImage;

    [SerializeField]
    private Renderer attackRangeObject; //Objet qui va servir a afficher la portée d'attaque
    [SerializeField]
    private Renderer movementRangeObject; //Objet qui va servir à afficher la portée de mouvement

    [SerializeField]
    private NetworkView networkView;
    
	// Use this for initialization
	void Start () {
       // m.SetInt("Range", 4);
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
                    selectedTarget = selectedCollider.GetComponent<BattleEntity>();
                    if(selectedTarget.IsEnemy)
                    {
                        if (selectedTarget.GetType() != typeof(BattleUnit))
                        {
                            selectedTarget.ChangeHP(-hoverCharacter.Power);
                        }
                        else
                        {
                            BattleUnit sel = (BattleUnit)selectedTarget;
                            sel.ChangeHP((hoverCharacter.Power - sel.Resist) * -1);
                        }
                            
                        hoverCharacter.TurnEnded = true;
                        SetRange(0, 0, new Vector3(0, -1f, 0));
                        moveCount = new Vector2(0, 0);
                        battleMain.battleState = BattleMain.Battlestate.waiting;
                        battleMain.IsTurnEndedForAll();
                    }
                    break;

                case BattleMain.Battlestate.selectingCharAction:
                    break;

                case BattleMain.Battlestate.selectingCharacter:
                    if(!isImpossiblePosition)
                    {
                        CharacterMoveValidation();
                        ActionButtonsGroup.gameObject.SetActive(true);
                        battleMain.battleState = BattleMain.Battlestate.selectingCharAction;
                    } 
                    break;

                case BattleMain.Battlestate.hoverCharacter:
                    if(!hoverCharacter.IsEnemy && hoverCharacter.PlayerN == input.currentPlayer)
                        battleMain.battleState = BattleMain.Battlestate.selectingCharacter;
                    break;

                case BattleMain.Battlestate.hoverGod:
                    if (!hoverGod.IsEnemy)
                    {
                        GodActionButtonsGroup.gameObject.SetActive(true);
                        battleMain.battleState = BattleMain.Battlestate.selectingGodAction;
                    }  
                    break;
                case BattleMain.Battlestate.selectingGodAction:
                    battleMain.battleState = BattleMain.Battlestate.waiting;
                    moveCount = new Vector2(0, 0);
                    battleMain.IsTurnEndedForAll();
                    break;

            }     
        }

        if (input.Bdown)
        {
            Canceling();
        }

        
        if(battleMain.battleState != BattleMain.Battlestate.selectingCharAction && battleMain.battleState != BattleMain.Battlestate.selectingGodAction && battleMain.battleState != BattleMain.Battlestate.selectingSideKickAction)
        {
            if (input.LeftDown)
            {
                adVal.x = -1; adVal.y = 0;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter || battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
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
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter || battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
                        moveCount.x++;

                    gameObject.transform.Translate(Vector3.right);
                }

            }

            if (input.DownDown)
            {
                adVal.x = 0; adVal.y = -1;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter || battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
                        moveCount.y--;

                    gameObject.transform.Translate(Vector3.back);
                }

            }
            if (input.UpDown)
            {
                adVal.x = 0; adVal.y = 1;
                if (isMovable())
                {
                    if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter || battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
                        moveCount.y++;

                    gameObject.transform.Translate(Vector3.forward);
                }

            }

            if(Input.GetMouseButtonDown(0))
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
        switch(battleMain.battleState)
        {
            case BattleMain.Battlestate.selectingCharacter:
                if ((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > hoverCharacter.Movement)
                    return false;
                break;

            case BattleMain.Battlestate.selectingAtkTarget:
                if ((Mathf.Abs(moveCount.x + adVal.x) + Mathf.Abs(moveCount.y + adVal.y)) > hoverCharacter.Range)
                    return false;
                break;

        }
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
		Debug.Log("Le Collider : " + col.tag);
		if(col.tag == "Character")
		{
            if (battleMain.battleState == BattleMain.Battlestate.waiting)
            {
                hoverCharacter = col.GetComponent<BattleUnit>();
                hoverCollider = col;
                selectedCharTransform = col.transform;
                if (!hoverCharacter.TurnEnded)
                {
                    battleMain.battleState = BattleMain.Battlestate.hoverCharacter;
                    RangeDisplay();

                }
            }
		}

        if (col.tag == "God")
        {
            if (battleMain.battleState == BattleMain.Battlestate.waiting)
            {
                hoverGod = col.GetComponent<BattleGod>();
                if (!hoverGod.TurnEnded)
                    battleMain.battleState = BattleMain.Battlestate.hoverGod;
            }
        }

        if (battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
        {
            selectedCollider = col;
            selectedTarget = col.gameObject.GetComponent<BattleUnit>();
        }
	}

	void OnTriggerExit(Collider col)
	{
		if(col.tag == "Character"){
            if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
            {
                if (col != hoverCollider)
                    isImpossiblePosition = true;
            }

            if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                isImpossiblePosition = false;

			if(battleMain.battleState == BattleMain.Battlestate.hoverCharacter)
			{
                SetRange(0, 0, new Vector3(0, -1f, 0));
				battleMain.battleState = BattleMain.Battlestate.waiting;
			}
		}

        if (col.tag == "God")
        {
            if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                isImpossiblePosition = false;

            if (battleMain.battleState == BattleMain.Battlestate.hoverGod)
            {
                battleMain.battleState = BattleMain.Battlestate.waiting;
            }
        }

        if (col.tag == "SideKick")
        {
            if (battleMain.battleState == BattleMain.Battlestate.selectingCharacter)
                isImpossiblePosition = true;

            if(battleMain.battleState == BattleMain.Battlestate.hoverSideKick)
            {
                battleMain.battleState = BattleMain.Battlestate.waiting;
            }

        }

	}

    void OnTriggerStay(Collider col)
    {
        if (battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
        {
            selectedCollider = col;
        }
    }

	void RangeDisplay()
	{
		if(battleMain.battleState == BattleMain.Battlestate.hoverCharacter)
		{
            if(!hoverCharacter.IsEnemy)
                SetRange(hoverCharacter.Movement, hoverCharacter.Range, new Vector3(hoverCharacter.transform.position.x, 0.01f, hoverCharacter.transform.position.z));
		}
	}
    void CharacterMoveValidation()
    {
        originalPos = selectedCharTransform.position;
        selectedCharTransform.position = new Vector3(gameObject.transform.position.x, selectedCharTransform.position.y, gameObject.transform.position.z);
        if (Network.isClient)
        {
            int ind=0;
            for (int i = 0; i < 5; i++)
            {
                if (selectedCharTransform == battleMain.Player2Entities[i].transform)
                {
                    ind = i;
                    break;
                }

            }
            networkView.RPC("CharacterMoveForClient", RPCMode.Server, selectedCharTransform.position, ind);
        }
            
        SetRange(0, 0, new Vector3(0,-1f,0));
        //hoverCharacter.HidePanels();
        SetRange(0, hoverCharacter.Range, hoverCharacter.transform.position);
        //hoverCharacter.ShowRangeForAttacking();
        adVal.x = 0; adVal.y = 0;
        moveCount.x = 0; moveCount.y = 0;
    }

    [RPC]
    void CharacterMoveForClient(Vector3 position, int characterIndex)
    {
        battleMain.clientPositions[characterIndex] = position;
    }
	
	void Canceling()
	{
        switch (battleMain.battleState)
        {
            case BattleMain.Battlestate.selectingAtkTarget:
                gameObject.transform.position = new Vector3(selectedCharTransform.position.x, gameObject.transform.position.y, selectedCharTransform.position.z);
                moveCount = new Vector2(0, 0);
                ActionButtonsGroup.gameObject.SetActive(true);
			    battleMain.battleState = BattleMain.Battlestate.selectingCharAction;
                break;

            case BattleMain.Battlestate.selectingCharacter:
                gameObject.transform.position= new Vector3(selectedCharTransform.position.x, gameObject.transform.position.y, selectedCharTransform.position.z);
                moveCount = new Vector2(0, 0);
                battleMain.battleState = BattleMain.Battlestate.hoverCharacter;
                break;

            case BattleMain.Battlestate.selectingCharAction:
                selectedCharTransform.position = originalPos;
                gameObject.transform.position = new Vector3(selectedCharTransform.position.x, gameObject.transform.position.y, selectedCharTransform.position.z);
                moveCount = new Vector2(0, 0);
                SetRange(0, 0, new Vector3(0, -1f, 0));
                SetRange(hoverCharacter.Movement, hoverCharacter.Range, hoverCharacter.transform.position);
                //hoverCharacter.ShowPanels();
                ActionButtonsGroup.SetActive(false);
			    battleMain.battleState = BattleMain.Battlestate.selectingCharacter;
                break;

            case BattleMain.Battlestate.selectingGodAction:
                GodActionButtonsGroup.SetActive(false);
                battleMain.battleState = BattleMain.Battlestate.hoverGod;
                break;

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
        SetRange(0, 0, new Vector3(0, -1f, 0));
        //hoverCharacter.HidePanels();
        battleMain.battleState = BattleMain.Battlestate.waiting;
        ActionButtonsGroup.gameObject.SetActive(false);

        battleMain.IsTurnEndedForAll();
    }

    public void SelectGodPower()
    {
        battleMain.battleState = BattleMain.Battlestate.waiting;
        GodActionButtonsGroup.gameObject.SetActive(false);

        battleMain.IsTurnEndedForAll();
    }

    public void SelectGodExplode()
    {
        hoverGod.Explode();
    }

    public void SelectGodSummon()
    {
        battleMain.battleState = BattleMain.Battlestate.waiting;
        GodActionButtonsGroup.gameObject.SetActive(false);

        battleMain.IsTurnEndedForAll();
    }

    public void SelectGodPowerUp()
    {
        battleMain.battleState = BattleMain.Battlestate.waiting;
        GodActionButtonsGroup.gameObject.SetActive(false);

        battleMain.IsTurnEndedForAll();
    }

    public void SelectGodItem()
    {
        battleMain.battleState = BattleMain.Battlestate.waiting;
        GodActionButtonsGroup.gameObject.SetActive(false);
        

        battleMain.IsTurnEndedForAll();

    }

    public void SelectGodEat()
    {

    }

    public void SelectGodRest()
    {
        hoverGod.ChangeHP(hoverGod.HPMax/10);

        battleMain.battleState = BattleMain.Battlestate.waiting;
        GodActionButtonsGroup.gameObject.SetActive(false);

        battleMain.IsTurnEndedForAll();
    }

    public void SetTextInfo()
    {
        if(battleMain.battleState == BattleMain.Battlestate.selectingAtkTarget)
        {
            if (selectedTarget != null)
            {
                hoverEntityNameText.text = selectedTarget.Nickname;
                hoverEntityHPText.text =  selectedTarget.HP + "/" + selectedTarget.HPMax;
                hoverEntityPowerText.text = "";
                hoverEntityResistText.text = "";
                hoverEntityHPImage.fillAmount = selectedTarget.HP*1.0f/selectedTarget.HPMax;
            }
                
        }
        else
        {
            if(battleMain.battleState == BattleMain.Battlestate.hoverCharacter)
            {
                if (hoverCharacter != null)
                {
                    hoverEntityNameText.text = hoverCharacter.Nickname;
                    hoverEntityHPText.text = hoverCharacter.HP + "/" + hoverCharacter.HPMax;
                    hoverEntityPowerText.text = hoverCharacter.Power.ToString();
                    hoverEntityResistText.text = hoverCharacter.Range.ToString();
                    hoverEntityHPImage.fillAmount = hoverCharacter.HP*1.0f / hoverCharacter.HPMax;
                }
            }

            if(battleMain.battleState == BattleMain.Battlestate.hoverGod)
            {
                if (hoverGod != null)
                {
                    hoverEntityNameText.text = hoverGod.Nickname;
                    hoverEntityHPText.text = hoverGod.HP + "/" + hoverGod.HPMax;
                    hoverEntityPowerText.text = "";
                    hoverEntityResistText.text = "";
                    hoverEntityHPImage.fillAmount = hoverGod.HP * 1.0f / hoverGod.HPMax;
                }
            }
          

                
        }
    }

    void FixedUpdate()
    {
        SetTextInfo();
    }

    void SetRange(int mvt, int atk, Vector3 pos)
    {
        attackRangeObject.material.SetInt("_Range", mvt + atk);
        movementRangeObject.material.SetInt("_Range", mvt);

        attackRangeObject.gameObject.transform.position = new Vector3(pos.x, pos.y - 0.001f, pos.z);
        movementRangeObject.gameObject.transform.position = pos;

    }
}
