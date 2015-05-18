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
            Defeat();
    }

    public void InvokeUnit(BattleUnit UnitType)
    {
        //Fonction qui va servir à gérer l'invocation d'une unité
        if (battleMain.PlayerEntities.Count < CountDisponibleUnits())
        {
            /*
            RaycastHit hit;
            float distanceToGround = 0;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0F))
                distanceToGround = hit.distance;
             */
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

    int CountDisponibleUnits()
    {
        int a=0;
        for(int i=0;i<battleMain.PlayerEntities.Count;i++)
        {
            if (battleMain.PlayerEntities[i].enabled)
                a++;
        }

        return a;
    }
}
