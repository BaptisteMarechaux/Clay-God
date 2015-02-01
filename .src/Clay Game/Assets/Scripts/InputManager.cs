using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
	[SerializeField]
	private Transform curs1;
	[SerializeField]
	private Transform curs2;

	class sPlayerIntents
	{
		public bool wantUpDown;
		public bool wantLeftDown;
		public bool wantRightDown;
		public bool wantDownDown;
		public bool wantADown;
		public bool wantBDown;
		public bool wantXDown;
		public bool wantYDown;
	}

	private Dictionary<NetworkPlayer, sPlayerIntents> playerIntents;
	private Dictionary<NetworkPlayer, sPlayerIntents> PlayerIntents
	{
		get{return playerIntents;}
		set{playerIntents = value;}
	}

	private NetworkView myNetworkView = null;

	bool leftDown;
	public bool LeftDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.LeftArrow))
				return true;
			if(leftDown)
			{
				leftDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{leftDown = value;}
	}

	bool rightDown;
	public bool RightDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.RightArrow))
				return true;
			if(rightDown)
			{
				rightDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{rightDown = value;}
	}

	bool downDown;
	public bool DownDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.DownArrow))
				return true;
			if(downDown)
			{
				downDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{downDown = value;}
	}

	bool upDown;
	public bool UpDown
	{
		get{
			if(Input.GetKeyDown(KeyCode.UpArrow))
				return true;
			if(upDown)
			{
				upDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{upDown = value;}
	}

	bool aDown;
	public bool Adown
	{
		get{
			if(Input.GetKeyDown(KeyCode.X))
				return true;
			if(aDown)
			{
				aDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{aDown = value;}
	}

	bool bDown;
	public bool Bdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.C))
				return true;
			if(bDown)
			{
				bDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{bDown = value;}
	}

	bool xDown;
	public bool Xdown
	{
		get{
			if(Input.GetKeyDown(KeyCode.V))
				return true;
			if(xDown)
			{
				xDown = false;
				return true;
			}
			else
			{
				return false;
			}
		}
		set{xDown = value;}
	}

	public IEnumerator Wait(float duration)
	{
		print(Time.time);
		yield return new WaitForSeconds (duration);
		print(Time.time);
	}

	void Start()
	{
		PlayerIntents = new Dictionary<NetworkPlayer, sPlayerIntents>();
		myNetworkView = this.gameObject.GetComponent<NetworkView>();
	}

	void OnPlayerConnected(NetworkPlayer p)
	{
		PlayerIntents.Add (p, new sPlayerIntents ());
		myNetworkView.RPC ("NewPlayerConnected", RPCMode.OthersBuffered, p);
	}

	[RPC]
	void NewPlayerConnected(NetworkPlayer p)
	{
		PlayerIntents.Add (p, new sPlayerIntents ());
	}

	//Il faut penser a faire l'update en fonction de l'exemple de la derniere fois
	void Update()
	{
		if(Network.isClient)
		{
			if(downDown)
				myNetworkView.RPC("wantDownDown", RPCMode.Server, Network.player, false);
			if(upDown)
				myNetworkView.RPC("wantUpDown", RPCMode.Server, Network.player, false);
			if(leftDown)
				myNetworkView.RPC("wantLeftDown", RPCMode.Server, Network.player, false);
			if(rightDown)
				myNetworkView.RPC("wantRightDown", RPCMode.Server, Network.player, false);
			if(aDown)
				myNetworkView.RPC("wantADown", RPCMode.Server, Network.player, false);
			if(bDown)
				myNetworkView.RPC("wantBDown", RPCMode.Server, Network.player, false);
		}

	}

	void FixedUpdate()
	{
		//Se servir de cette méthode pour les mouvements et autres inputs
	}

	[RPC]
	void PlayerWantToMoveUp(NetworkPlayer p, bool b)
	{
		PlayerIntents [p].wantUpDown = b;
		if (Network.isServer)
		{
			myNetworkView.RPC("wantUpDown", RPCMode.Others, p, b);
		}
	}
	
	[RPC]
	void PlayerWantToMoveDown(NetworkPlayer p, bool b)
	{
		PlayerIntents[p].wantDownDown = b;
		if (Network.isServer)
		{
			myNetworkView.RPC("PlayerWantToMoveDown", RPCMode.Others, p, b);
		}
	}
}
