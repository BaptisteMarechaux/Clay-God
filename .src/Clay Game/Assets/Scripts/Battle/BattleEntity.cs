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

    protected Color trueColor;

    protected bool turnEnded;
    public bool TurnEnded
    {
        get { return turnEnded; }
        set { 
            turnEnded = value;
            if(turnEnded)
            {
                //On montre que l'entité est inactive
                gameObject.renderer.material.color = new Color(trueColor.r * 0.3f,trueColor.g * 0.3f, trueColor.b * 0.3f);
            }
            else
            {
                //On montre que l'entité est à nouveau active
                gameObject.renderer.material.color = trueColor;
            }
        }
    }


	public virtual void Start()
	{
		hp = hpMax;
        trueColor = gameObject.renderer.material.color;
	}

	public virtual void ChangeHP(int amount)
	{
		hp+=amount;
		if(hp<0) hp=0;
		if(hp>hpMax) hp = hpMax;
       
	}

		
}
