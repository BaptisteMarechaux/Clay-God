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

    int[] playerItemCount;
    ShopItem[] itemList;

	void Start () {
        playerItemCount = saveScript.PlayerInfo.ItemCount;
        itemList = saveScript.ShopItems;
        oldTarget = cameraScript.target;
        for(int i=0;i<itemNames.Length;i++)
        {
            itemNames[i].text = itemList[i].ItemName;
            itemQuantity[i].text = playerItemCount[i].ToString();

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveToUnit(int unitIndex)
    {
        cameraScript.target.position = new Vector3(battleMain.PlayerEntities[unitIndex].gameObject.transform.position.x, cameraScript.target.position.y, battleMain.PlayerEntities[unitIndex].gameObject.transform.position.z);
    }

    public void ShowItemUseMenu()
    {
        godActionsCanvas.SetActive(false);
        itemUseCanvas.SetActive(true);

    }
    public void HideItemUseMenu()
    {
        cameraScript.target = oldTarget;
    }

    public void UseItem(int unitIndex)
    {

    }
}
