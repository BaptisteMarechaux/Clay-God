using UnityEngine;
using System.Collections;

public abstract class BattleEntity : MonoBehaviour {
	[SerializeField]
	protected int hp;
    public int HP
    {
        get { return hp; }
    }
	[SerializeField]
	protected int hpMax;
    public int HPMax
    {
        get { return hpMax; }
    }
	[SerializeField]
	protected string nickname;
    public string Nickname
    {
        get { return nickname; }
    }

	[SerializeField]
	protected bool isEnemy;
	public bool IsEnemy{
		get{return isEnemy;}
	}

    protected bool turnEnded;
    public bool TurnEnded
    {
        get { return turnEnded; }
        set { turnEnded = value; }
    }

	public virtual void Start()
	{
		hp = hpMax;
	}

	public virtual void ChangeHP(int amount)
	{
		hp+=amount;
		if(hp<0) hp=0;
		if(hp>hpMax) hp = hpMax;
	}

		
}
