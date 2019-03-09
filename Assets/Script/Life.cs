using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	UILabel label;
	GameObject dataObj;
	GameData data;
	// Use this for initialization
	void Start () 
	{
		label = GetComponent<UILabel>();
		dataObj = GameObject.FindWithTag("GAMEDATA");
		data = dataObj.GetComponent<GameData>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		label.text = "x " + data.life;
	}
}
