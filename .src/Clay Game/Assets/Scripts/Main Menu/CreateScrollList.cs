using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[System.Serializable]
public class ScrollListItem{
    public string itemName;
    public string playerCount;
    public bool isFull;
    public Button.ButtonClickedEvent itemAction;
    public HostData hostData;
}

public class CreateScrollList : MonoBehaviour {
    public GameObject sampleButton;
    public List<ScrollListItem> serversList;
    public Transform controlPanel;

    public List<GameObject> createdButtons;

    void Awake()
    {
        createdButtons = new List<GameObject>();
    }

	// Use this for initialization
	void Start () {
        
        //ListCreationBegin();
	}
	
	public void ListCreationBegin()
    {
        if(createdButtons.Count>0)
        {
            ListDestroy();
        }
        foreach(var item in serversList)
        {
            GameObject newButton = Instantiate(sampleButton) as GameObject;
            SampleButton sButton = newButton.GetComponent<SampleButton>();
            sButton.nameText.text = item.itemName;
            sButton.playerCountText.text = "Number of Players"  + item.playerCount;
           
            newButton.GetComponent<StandaloneJoinParty>().hostData = item.hostData;
            //sButton.button.onClick = item.itemAction;
            newButton.transform.SetParent(controlPanel);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.localPosition= new Vector3(newButton.transform.localPosition.x, newButton.transform.localPosition.y, 0);
            createdButtons.Add(newButton);
        }
    }

    public void ListDestroy()
    {
        for(int i=0;i<createdButtons.Count;i++)
        {
            GameObject.Destroy(createdButtons[i]);
        }
        createdButtons.Clear();
    }
}
