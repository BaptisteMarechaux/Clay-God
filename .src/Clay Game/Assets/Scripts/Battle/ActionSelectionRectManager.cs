using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionSelectionRectManager : MonoBehaviour {
    [SerializeField]
    private List<Transform> ActionButtons;

    [SerializeField]
    private List<Transform> ActiveActionButtons;

    [SerializeField]
    private BattleMain battleMain;
    [SerializeField]
    private InputManager input;

    private int currentIndex;
    public int CurentIndex
    {
        get { return currentIndex; }
        set
        {
            currentIndex = value;
            if (currentIndex < 0)
                currentIndex = 0;
            if (currentIndex >= ActiveActionButtons.Count)
                currentIndex = ActiveActionButtons.Count - 1;

        }
    }
	// Use this for initialization
	void Start () {
        ActiveActionButtons = new List<Transform>();
        for (int i = 0; i < ActionButtons.Count; i++)
        {
            if (ActionButtons[i].gameObject.activeSelf)
            {
                ActiveActionButtons.Add(ActionButtons[i]);
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (input.LeftDown)
        {

        }
        if (input.RightDown)
        {

        }
	}
}
