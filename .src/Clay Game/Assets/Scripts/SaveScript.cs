using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveScript : MonoBehaviour {

    [SerializeField]
    ShopItem[] shopItems;
    public ShopItem[] ShopItems
    {
        get { return shopItems; }
    }
    
    [SerializeField]
    PlayerInfos playerInfo;
    public PlayerInfos PlayerInfo
    {
        get { return playerInfo; }
    }

    void Awake()
    {
        LoadPlayerInfos();
        LoadItemInfos();
    }

    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadPlayerInfos()
    {
        var filePath = Application.persistentDataPath +
            "pinf.bin";
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var fs = File.OpenRead(filePath);

            playerInfo = (PlayerInfos)formatter.Deserialize(fs);

            fs.Close();
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var fs = File.Create(filePath);

            formatter.Serialize(fs, playerInfo);

            fs.Close();

            LoadPlayerInfos();
        }
    }

    public void LoadItemInfos()
    {
        var filePath = Application.persistentDataPath + "shopItemDescription.bin";
        if(File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var fs = File.OpenRead(filePath);

            shopItems = (ShopItem[])formatter.Deserialize(fs);

            fs.Close();
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            var fs = File.Create(filePath);

            formatter.Serialize(fs, shopItems);
            fs.Close();
            LoadItemInfos();
        }
    }

    public void SavePlayerInfos(string pname, int pmoney, int plevel, int[] pitemcount)
    {
        playerInfo.PlayerName = pname;
        playerInfo.Money = pmoney;
        playerInfo.ItemCount = pitemcount;
        playerInfo.CurrentLevel = plevel;

        var filePath = Application.persistentDataPath + "pinf.bin";

        BinaryFormatter formatter = new BinaryFormatter();
        var fs = File.Create(filePath);

        formatter.Serialize(fs, playerInfo);
        fs.Close();

    }

    void initializeItemInfos()
    {
        shopItems = new ShopItem[]{
            new ShopItem(0, "Potion", 20, "Restores 30% of HP"),
            new ShopItem(0, "HyperPotion", 60, "Restores all HP"),
            new ShopItem(0, "Poison", 40, "Takes 50% of HP"),
            new ShopItem(0, "UpPower", 50, "Raise Power by 2"),
            new ShopItem(0, "UpResistance", 50, "Raise Resistance by 2"),
            new ShopItem(0, "UpMovement", 50, "Raise Movement by 2"),
            new ShopItem(0, "DownPower", 20, "Reduce Power of an enemy by 2"),
            new ShopItem(0, "DownResistance", 20, "Reduce Resistance of an enemy by 2"),
            new ShopItem(0, "ImmunePotion", 100, "Ignore one attack")
        };
    }
}
