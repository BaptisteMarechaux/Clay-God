using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerInfos
{
    [SerializeField]
    string playerName;
    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    [SerializeField]
    int money;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    [SerializeField]
    int currentLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    [SerializeField]
    int[] itemCount; //Chaque item possède un ID unique et à chaque index d'Item count correspond la quantité obtenue d'un objet
    public int[] ItemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }


}
