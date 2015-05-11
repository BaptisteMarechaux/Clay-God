using UnityEngine;
using System.Collections;

public class StandaloneJoinParty : MonoBehaviour {
    public HostData hostData;
	public void JoinServer()
    {
        NetworkManager.GameToJoin = hostData;
        Application.LoadLevel(2);
    }
}
