using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleGod : BattleEntity {

    [SerializeField]
    public GameObject SpecialPowerEffect;

    private int CP; //Custom Points : Points servant à customiser le Dieu
    private int IP; //Invoke Point : Point servant d'appeler à la création d'unités.

    [SerializeField]
    public List<BattleUnit> possibleUnits;

    [SerializeField]
    private int explosionRange;
    public int ExplosionRange
    {

        get { return explosionRange; }

        set { explosionRange = value; }
    }

	// Use this for initialization
	public override void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
        
           
	}

    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (hp <= 0)
        {
            Defeat();
            Debug.Log("hp = 0");
        }
           
    }

    public void InvokeUnit()
    {
        //Fonction qui va servir à gérer l'invocation d'une unité
        if (5 > CountDisponibleUnits(battleMain.input.currentPlayer))
        {
            for (int i = 0; i < 5; i++)
            {
                if (battleMain.input.currentPlayer == 1)
                {
                    if (!battleMain.PlayerEntities[i].gameObject.activeSelf)
                    {
                        battleMain.PlayerEntities[i].ChangeHP(999);
                        battleMain.PlayerEntities[i].gameObject.SetActive(true);
                        battleMain.PlayerEntities[i].transform.position = transform.position + Vector3.back;
                        break;
                    }
                }

                if (battleMain.input.currentPlayer == 2)
                {
                    if (!battleMain.Player2Entities[i].gameObject.activeSelf)
                    {
                        battleMain.Player2Entities[i].ChangeHP(999);
                        battleMain.Player2Entities[i].gameObject.SetActive(true);
                        battleMain.Player2Entities[i].transform.position = transform.position + Vector3.back;
                        break;
                    }
                }

            }
        }
    }

    public void Explode()
    {
        //Fonction qui va gérer l'explosion d'un Dieu , causant des effets aux alentours
        ChangeHP(-999);
    }

    public void Rest()
    {
        //Fonction qui va permettre le repos qui est permi une fois par tour pour le Dieu
        //Rend actuellement 50% des hpMax
        ChangeHP(Mathf.FloorToInt(hpMax * 0.5f));
        turnEnded = true;
    }

    public void Defeat()
    {
        if(!isEnemy)
        {
            battleMain.Defeat();
        }
        else
        {
            //Victory
            battleMain.Victory();
        }
        
    }

    int CountDisponibleUnits(int player)
    {
        int a=0;
        for(int i=0;i<5;i++)
        {
            if (player == 1)
            {
                if (battleMain.PlayerEntities[i].gameObject.activeSelf)
                    a++;
            }

            if (player == 2)
            {
                if (battleMain.Player2Entities[i].gameObject.activeSelf)
                    a++;
            }
            
        }

        return a;
    }
}
