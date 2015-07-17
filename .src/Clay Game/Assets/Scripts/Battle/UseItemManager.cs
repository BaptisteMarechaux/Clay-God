using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UseItemManager : MonoBehaviour {
    [SerializeField]
    BattleMain battleMain;

    [SerializeField]
    CameraScript cameraScript;

    [SerializeField]
    SaveScript saveScript;

    Transform oldTarget;

    [SerializeField]
    GameObject godActionsCanvas;

    [SerializeField]
    GameObject itemUseCanvas;

    [SerializeField]
    Text[] itemNames;

    [SerializeField]
    Text[] itemQuantity;

    [SerializeField]
    Image[] itemImage;

    [SerializeField]
    Text[] unitsNameText;

    [SerializeField]
    Text[] unitsHPText;

    [SerializeField]
    Text[] unitsPowText;

    [SerializeField]
    Text[] unitsResText;

    [SerializeField]
    Text[] unitsMovText;

    [SerializeField]
    Text descriptionText;

    int[] playerItemCount;
    ShopItem[] itemList;
    BattleUnit[] unitList = new BattleUnit[10];
    int unitCount;

    int selectedItem=0;

	void Start () {
        descriptionText.text = "Chose an item and Select a target unit";
        playerItemCount = saveScript.PlayerInfo.ItemCount;
        itemList = saveScript.ShopItems;
        oldTarget = cameraScript.target;
        for(int i=0;i<itemNames.Length;i++)
        {
            itemNames[i].text = itemList[i].ItemName;
            itemQuantity[i].text = "x" + playerItemCount[i].ToString();


        }
        unitCount = 0;
        for(int i=0;i<5;i++)
        {
            if (battleMain.PlayerEntities[i].gameObject.activeSelf)
            {
                unitsNameText[unitCount].text = battleMain.PlayerEntities[i].Nickname;
                unitsPowText[unitCount].text = "Pow: " + battleMain.PlayerEntities[i].Power.ToString();
                unitsResText[unitCount].text = "Res: " + battleMain.PlayerEntities[i].Resist.ToString();
                unitsMovText[unitCount].text = "Mov: " + battleMain.PlayerEntities[i].Movement.ToString();
                unitsHPText[unitCount].text = "HP:" + battleMain.PlayerEntities[i].HP + "/" + battleMain.PlayerEntities[i].HPMax;
                unitList[unitCount] = battleMain.PlayerEntities[i];
                unitCount++;
            }
            
        }

        for (int i = 0; i < 5; i++)
        {
            if (battleMain.Player2Entities[i].gameObject.activeSelf)
            {
                unitsNameText[unitCount].text = battleMain.Player2Entities[i].Nickname;
                unitsPowText[unitCount].text = "Pow: " + battleMain.Player2Entities[i].Power.ToString();
                unitsResText[unitCount].text = "Res: " + battleMain.Player2Entities[i].Resist.ToString();
                unitsMovText[unitCount].text = "Mov: " + battleMain.Player2Entities[i].Movement.ToString();
                unitsHPText[unitCount].text = "HP:" + battleMain.Player2Entities[i].HP + "/" + battleMain.Player2Entities[i].HPMax;
                unitList[unitCount] = battleMain.Player2Entities[i];
                unitCount++;
            }
        }

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveToUnit(int unitIndex)
    {
        cameraScript.target.position = new Vector3(unitList[unitIndex].gameObject.transform.position.x, cameraScript.target.position.y, unitList[unitIndex].gameObject.transform.position.z);
    }

    public void ShowItemUseMenu()
    {
        godActionsCanvas.SetActive(false);
        itemUseCanvas.SetActive(true);
        for (int i = 0; i < itemNames.Length; i++)
        {
            itemQuantity[i].text = "x" + playerItemCount[i].ToString();


        }


        unitList = new BattleUnit[10];

        unitCount = 0;
        for (int i = 0; i < 5; i++)
        {
            if (battleMain.PlayerEntities[i].gameObject.activeSelf)
            {
                unitsNameText[unitCount].text = battleMain.PlayerEntities[i].Nickname;
                unitsPowText[unitCount].text = "Pow: " + battleMain.PlayerEntities[i].Power.ToString();
                unitsResText[unitCount].text = "Res: " + battleMain.PlayerEntities[i].Resist.ToString();
                unitsMovText[unitCount].text = "Mov: " + battleMain.PlayerEntities[i].Movement.ToString();
                unitsHPText[unitCount].text = "HP:" + battleMain.PlayerEntities[i].HP + "/" + battleMain.PlayerEntities[i].HPMax;
                unitList[unitCount] = battleMain.PlayerEntities[i];
                unitCount++;
            }

        }

        for (int i = 0; i < 5; i++)
        {
            if (battleMain.Player2Entities[i].gameObject.activeSelf)
            {
                unitsNameText[unitCount].text = battleMain.Player2Entities[i].Nickname;
                unitsPowText[unitCount].text = "Pow: " + battleMain.Player2Entities[i].Power.ToString();
                unitsResText[unitCount].text = "Res: " + battleMain.Player2Entities[i].Resist.ToString();
                unitsMovText[unitCount].text = "Mov: " + battleMain.Player2Entities[i].Movement.ToString();
                unitsHPText[unitCount].text = "HP:" + battleMain.Player2Entities[i].HP + "/" + battleMain.Player2Entities[i].HPMax;
                unitList[unitCount] = battleMain.Player2Entities[i];
                unitCount++;
            }
        }

    }
    public void HideItemUseMenu()
    {
        cameraScript.target = oldTarget;
        itemUseCanvas.SetActive(false);
        battleMain.battleState = BattleMain.Battlestate.waiting;
    }

    public void SelectItem(int index)
    {
        descriptionText.text = "Selected Item : " + itemList[index].ItemName + " : " + itemList[index].ItemDescription;
    }

    public void UseItem(int unitIndex)
    {
        if (unitIndex >= unitCount)
            return;
        if (playerItemCount[selectedItem] == 0)
        {
            descriptionText.text = "You don't have enough " + itemList[selectedItem].ItemName;
        }
        int AmountToChange=0;

        switch (itemList[selectedItem].effectValue)
        {
            case ShopItem.itemEffectValue.Percent30:
                AmountToChange = Mathf.RoundToInt(unitList[unitIndex].HPMax * 0.3f);
                break;

            case ShopItem.itemEffectValue.Full:
                AmountToChange = 999;
                break;

            case ShopItem.itemEffectValue.One:
                AmountToChange = 1;
                break;

            case ShopItem.itemEffectValue.Two:
                AmountToChange = 2;
                break;

            case ShopItem.itemEffectValue.Three:
                AmountToChange = 3;
                break;
        }

        switch (itemList[selectedItem].effect)
        {
            case ShopItem.itemEffect.HPDown:
                unitList[unitIndex].ChangeHP(-AmountToChange);
                break;
            case ShopItem.itemEffect.HPUp:
                unitList[unitIndex].ChangeHP(AmountToChange);
                break;

            case ShopItem.itemEffect.MovUp:
                unitList[unitIndex].Movement += AmountToChange;
                break;

            case ShopItem.itemEffect.PowUp:
                unitList[unitIndex].Power += AmountToChange;
                break;

            case ShopItem.itemEffect.PowDwn:
                unitList[unitIndex].Power -= AmountToChange;
                break;

            case ShopItem.itemEffect.ResUp:
                unitList[unitIndex].Resist+= AmountToChange;
                break;

            case ShopItem.itemEffect.ResDwn:
                unitList[unitIndex].Resist -= AmountToChange;
                break;
        }
        
        playerItemCount[selectedItem] -= 1;
        itemQuantity[selectedItem].text = "x" + playerItemCount[selectedItem].ToString();
        saveScript.SavePlayerInfos(saveScript.PlayerInfo.PlayerName, saveScript.PlayerInfo.Money, saveScript.PlayerInfo.CurrentLevel, playerItemCount);
    }
}
