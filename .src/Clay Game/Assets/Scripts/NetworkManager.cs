using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	//Network 
	public const string _typeName = "TowerDefense";
	public string _gameName = "ClayGame";
	public const int _nbPlayers = 2;
	private HostData[] _hostData;
	public static HostData _gameData;
	private int _port = 6600;
	public string _serverIP = "127.0.0.1";
	[SerializeField]
	Camera _serverCamera;

	void Start()
	{
		Application.runInBackground = true;
	}
	

	public void OnMasterServerEvent(MasterServerEvent sEvent)
	{
		if (sEvent == MasterServerEvent.HostListReceived)
			_hostData = MasterServer.PollHostList();
	}
	
	public void StartServer()
	{
		try{
			if (!Network.isClient && !Network.isServer) {
				Network.InitializeSecurity();
				Network.InitializeServer (_nbPlayers, _port, !Network.HavePublicAddress ());
				MasterServer.RegisterHost (_typeName, _gameName);
				Debug.Log("Server client connected !");
			}
		}catch(UnityException e){
			Debug.Log (e.Message);
				}
	}

	public void StartClient ()
	{
		try {
			Network.Connect (_serverIP,(_port));
		} catch (UnityException e) {
			Debug.LogError (e.Message);
		}
	}
	
	public void RefreshHostList()
	{
		MasterServer.ClearHostList();
		MasterServer.RequestHostList (_typeName);
		
		if (MasterServer.PollHostList().Length != 0) {
			HostData[] hostData = MasterServer.PollHostList();
			int i = 0;
				while (i < hostData.Length) {
					Debug.Log("Game name: " + hostData[i].gameName);
					i++;
				}
			MasterServer.ClearHostList();
		}
	}
		
		public void JoinServer(HostData _gameToJoin)
		{
			_gameData = _gameToJoin; //Les données qui correspondent à la partie qu'on veut rejoindre
		Application.LoadLevel("TacticalMovementTestScene"); //Le niveau à charger
		}
		
		
		public void OnConnectedToServer() // Connexion du client
		{
			Debug.Log("Connected to server");
			Application.LoadLevel("TacticalMovementTestScene");//Le niveau à charger
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