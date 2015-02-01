using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	private bool isServer ;
	public bool IsServer
	{
		get{return isServer;}
		set{isServer = value;}
	}

	void Start()
	{
		if(IsServer)
		{
			Network.InitializeSecurity();
			Network.InitializeServer(2, 6600, true);
		}
		else
		{
			Network.Connect("127.0.0.1", 6600); //Ca connecte en local , mais on va changer la valeur de l'adresse de référence
		}
	}
	
	
}