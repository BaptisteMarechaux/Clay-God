using UnityEngine;
using System.Collections;
using MyFirstUnityLibrary;

public class TestNETLibScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MyFirstUnityLibrary.MyClass caca = new MyClass();

        caca.SayWhat();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
