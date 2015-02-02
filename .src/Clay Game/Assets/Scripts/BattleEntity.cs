using UnityEngine;
using System.Collections;

public abstract class BattleEntity : MonoBehaviour {
	[SerializeField]
	protected int hp;
	[SerializeField]
	protected int hpMax;
	[SerializeField]
	protected string nickname;

	[SerializeField]
	protected bool isEnemy;
	public bool IsEnemy{
		get{return isEnemy;}
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
