using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour {
	GameData data;
	
	// Use this for initialization
	void Start () 
	{
		data = GameObject.FindWithTag("GAMEDATA").GetComponent<GameData>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		UILabel ui = GetComponent<UILabel>();
		ui.text = "x "+ data._stageCoins;
	}
}
