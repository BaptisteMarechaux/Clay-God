using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	//Network Part
	public const string _typeName = "TowerDefense";
	public string _gameName;
	public const int _nbPlayers = 2;
	private HostData[] _hostData;
	public static HostData _gameData;
	private int _port = 6600;

	//Game
	public GameObject playerPrefab;
	
	void Start()
	{
	}

	public void SpawnPlayer()
	{

	}

	void OnGUI ()
	{
		
		// Tout pendant que le réseau n'est pas initialisé
		if (!(Network.isServer ^ Network.isClient)) {
			GUILayout.BeginVertical ();
			
			GUILayout.BeginHorizontal ();
			
			GUILayout.Label ("Server IP : ");
			
			_serverIP = GUILayout.TextField (_serverIP);
			
			GUILayout.Label ("Server Port : ");
			
			_serverPort = GUILayout.TextField (_serverPort);
			
			if (GUILayout.Button ("Start Client")) {
				StartClient ();
			}
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			
			GUILayout.Label ("Number Of Players (1-5) : ");
			
			_playerNumber = GUILayout.TextField (_playerNumber);
			
			GUILayout.Label ("Server Port : ");
			
			_serverPort = GUILayout.TextField (_serverPort);
			
			if (GUILayout.Button ("Start Server")) {
				StartServer ();
			}
			
			GUILayout.EndHorizontal ();
			
			GUILayout.EndVertical ();
		}
	}

	void OnMasterServerEvent(MasterServerEvent sEvent)
	{
		if (sEvent == MasterServerEvent.HostListReceived)
			_hostData = MasterServer.PollHostList();
	}
	
	public void StartServer()
	{
		if (!Network.isClient && !Network.isServer) {
			Network.InitializeSecurity();
			Network.InitializeServer (_nbPlayers, _port, !Network.HavePublicAddress ());
			MasterServer.RegisterHost (_typeName, _gameName);
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
			Application.LoadLevel(""); //Le niveau à charger
		}
		
		
		public void OnConnectedToServer()
		{
			Debug.Log("Connected to server");
			Application.LoadLevel("");//Le niveau à charger
		}
		
		public void OnFailedToConnectToMasterServer(NetworkConnectionError exception)
		{
			Debug.Log("Fail to connect master server : " + exception);
		}
		
		public void OnFailedToConnect(NetworkConnectionError exception)
		{
			Debug.Log("Fail to connect server: " + exception);
		}
		
		
		public void OnPlayerConnected(NetworkPlayer player) //Server
		{
			Debug.Log("Connected !");
			
		}
		
		
	}