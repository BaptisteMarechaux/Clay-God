using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NetworkManager : MonoBehaviour {
	
	//Network 
	public const string typeName = "DeadlySufferingKawaiClayGod";
	public string gameName = "ClayGame";
	public const int nbPlayers = 2;
	private HostData[] hostData;
	public static HostData GameToJoin;
	private int port = 6600;
	public string serverIP = "127.0.0.1";
	[SerializeField]
	Camera serverCamera;

    [SerializeField]
    CreateScrollList scrollList;

    [SerializeField]
    Text nameOfGame;

	void Start()
	{
        MasterServer.RequestHostList(typeName);
	}
	

	public void OnMasterServerEvent(MasterServerEvent sEvent)
	{
		if (sEvent == MasterServerEvent.HostListReceived)
			hostData = MasterServer.PollHostList();
	}
	
	public void StartServer()
	{
		try{
			if (!Network.isClient && !Network.isServer) {
				Network.InitializeSecurity();
				Network.InitializeServer (nbPlayers, port, !Network.HavePublicAddress ());
                gameName = nameOfGame.text != "" ? nameOfGame.text : gameName;
				MasterServer.RegisterHost (typeName, gameName);
				Debug.Log("Server client connected !");
			}
		}catch(UnityException e){
			Debug.Log (e.Message);
				}
	}

	public void StartClient ()
	{
		try {
			Network.Connect (serverIP,(port));
		} catch (UnityException e) {
			Debug.LogError (e.Message);
		}
	}
	
	public void RefreshHostList()
	{
		//MasterServer.ClearHostList();
		MasterServer.RequestHostList (typeName);
		
		if (MasterServer.PollHostList().Length != 0) {
			HostData[] hostData = MasterServer.PollHostList();
                for (int i = 0, l = hostData.Length; i < l; i++)
                {
                    Debug.Log("Game name: " + hostData[i].gameName);
                    scrollList.serversList.Add(new ScrollListItem());
                    scrollList.serversList[i].itemName = hostData[i].gameName;
                    scrollList.serversList[i].playerCount = hostData[i].connectedPlayers.ToString();
                    scrollList.serversList[i].hostData = hostData[i];
         
                }
            scrollList.ListCreationBegin();
                //MasterServer.ClearHostList();
		}
	}
		
		public void JoinServer(HostData gameToJoin)
		{
			//GameToJoin = gameToJoin; //Les données qui correspondent à la partie qu'on veut rejoindre
            
            for (int i = 0, l = hostData.Length; i < l; i++)
            {
                if(hostData[i]!=null)
                    GameToJoin = hostData[i];
            }
            
		    //Application.LoadLevel("TacticalMovementTestScene"); //Le niveau à charger
		}
		
		
		public void OnConnectedToServer() // Connexion du client
		{
			Debug.Log("Connected to server");
			//Application.LoadLevel("TacticalMovementTestScene");//Le niveau à charger
		}
		
		void OnServerInitialized() // Connexion du client
		{ 
			Application.LoadLevel("TacticalMovementTestScene");  
		}
		
		public void OnFailedToConnectToMasterServer(NetworkConnectionError exception) // Erreur à la connexion du master server
		{
			Debug.Log("Fail to connect master server : " + exception);
		}
		
		public void OnFailedToConnect(NetworkConnectionError exception) // Erreur à la connexion au serveur
		{
			Debug.Log("Fail to connect server: " + exception);
		}
		
		
		public void OnPlayerConnected() // Connexion Server
		{
			Debug.Log("Connected !");
			
		}

       
	}