using UnityEngine;
using System.Collections;

public abstract class BattleEntity : MonoBehaviour {
	protected int hp;
	protected int hpMax;
	protected int nickname;

	public virtual void ChangeHP(int amount)
	{
		hp+=amount;
		if(hp<0) hp=0;
		if(hp>hpMax) hp = hpMax;
	}
		
}
