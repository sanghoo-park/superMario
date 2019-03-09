using UnityEngine;
using System.Collections;

public class StartMenneger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
		
	void onStart()
	{
		Debug.Log("start");
		Application.LoadLevel(1);
	}
}
