using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[System.Serializable]
public class ShopItem{
    [SerializeField]
    private int id; //ID Unique pour chaque objet
    public int Id
    {
        get { return id; }
    }

    [SerializeField]
    private string itemName;
    public string ItemName
    {
        get { return itemName; }
    }

    [SerializeField]
    private int itemCost;
    public int ItemCost
    {
        get { return itemCost; }
    }

    [SerializeField]
    private string itemDescription;
    public string ItemDescription
    {
        get {return itemDescription; }
    }

    public enum itemEffect
    {
        HPUp,
        HPDown,
        PowUp,
        ResUp,
        MovUp,
        PowDwn,
        ResDwn,

    }

    public enum itemEffectValue
    {
        Percent30,
        Full,
        One,
        Two,
        Three
    }

    public itemEffect effect;

    public itemEffectValue effectValue;

    public ShopItem(int _id, string _name, int _cost, string _description)
    {
        id = _id;
        itemName = _name;
        itemCost = _cost;
        itemDescription = _description;
    }

}
