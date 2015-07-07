using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class TestNativeLibScript : MonoBehaviour {
    [DllImport("MyFirstNativeDllForUnity")]
    public extern static int Caca(int arg);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
