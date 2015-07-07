using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BoutiqueManager : MonoBehaviour {

    [SerializeField]
    private ShopItem[] itemList;

    [SerializeField]
    SaveScript save;

    private int money;
    public int Money
    {
        set { money = value; }
    }

    //Elements d'UI pour tous les objets
    [SerializeField]
    private Text MoneyText;
    [SerializeField]
    private Text[] ItemNames;
    [SerializeField]
    public Image[] ItemImages;
    [SerializeField]
    private Text[] ItemCosts;
    [SerializeField]
    private Text[] ItemCountText;

    //Elements d'UI pour l'objet sélectionné
    [SerializeField]
    private Text headerItemText;
    [SerializeField]
    private Text buyMoneyText;
    [SerializeField]
    private Text buyItemName;
    [SerializeField]
    private Image buyItemImage;
    [SerializeField]
    private Text buyItemCost;
    [SerializeField]
    private Text buyItemDescription;
    [SerializeField]
    private Text buyItemCount;


    [SerializeField]
    private GameObject BuyItemCanvas;
    [SerializeField]
    private GameObject ItemListCanvas;

    int selectedItemID;
    int[] playerItemCount;

	// Use this for initialization
	void Start () {
        playerItemCount = save.PlayerInfo.ItemCount;
        itemList = save.ShopItems;
        money = save.PlayerInfo.Money;

        MoneyText.text = money.ToString()+"£";

        for (int i = 0; i < itemList.Length; i++)
        {
            ItemNames[i].text = itemList[i].ItemName;
            ItemCosts[i].text = itemList[i].ItemCost.ToString()+"£";
            ItemCountText[i].text = "x" + playerItemCount[i].ToString();

            if (i >= ItemNames.Length)
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowBuyingWidnow(int selId)
    {
        ItemListCanvas.SetActive(false);
        BuyItemCanvas.SetActive(true);

        selectedItemID = selId;
        buyMoneyText.text = money.ToString() + "£";
        headerItemText.text = itemList[selectedItemID].ItemName;
        buyItemName.text = itemList[selectedItemID].ItemName;
        //buyItemImage;
        buyItemDescription.text = itemList[selectedItemID].ItemDescription;
        buyItemCost.text = itemList[selectedItemID].ItemCost.ToString() + "£";
        buyItemCount.text = "x" + playerItemCount[selectedItemID].ToString();
    }

    public void Buy()
    {
        if (itemList[selectedItemID].ItemCost <= money)
        {
            //Ajouter l'objet à l'inventaire
            money -= itemList[selectedItemID].ItemCost;
            playerItemCount[selectedItemID]+=1;
            buyMoneyText.text = money.ToString() + "£";
            buyItemCount.text = "x" + playerItemCount[selectedItemID].ToString();

            save.SavePlayerInfos(save.PlayerInfo.PlayerName, money, save.PlayerInfo.CurrentLevel, playerItemCount);
        }
        else
        {
            buyItemDescription.text = "Not enough money";
        }
    }

    public void BackFromItemBuying()
    {
        ItemListCanvas.SetActive(true);
        BuyItemCanvas.SetActive(false);

        MoneyText.text = money.ToString() + "£";

        for (int i = 0; i < itemList.Length; i++)
        {
            ItemNames[i].text = itemList[i].ItemName;
            ItemCosts[i].text = itemList[i].ItemCost.ToString() + "£";
            ItemCountText[i].text = "x" + playerItemCount[i].ToString();

            if (i >= ItemNames.Length)
                break;
        }
    }
}
