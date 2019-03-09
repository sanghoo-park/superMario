using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	UILabel label;
	string _clearTime;
	float sec = 0;
	float persec = 0;
	GameData data;
	// Use this for initialization
	void Start () 
	{
		data = GameObject.FindWithTag("GAMEDATA").GetComponent<GameData>();
		label = GetComponent<UILabel>();
		label.text = "0:00";
	}
	
	// Update is called once per frame
	void Update () 
	{
		persec += float.Parse(Time.deltaTime.ToString("N2"));
		if(persec > 1)
		{
			persec = 0;
			sec += 1;
		}
		if(GameMenneger.instance._flagHit == false) {
			label.text = sec + ":" + float.Parse(persec.ToString("N2")) * 100;
			_clearTime = sec + ":" + float.Parse(persec.ToString("N2")) * 100;
		}else{
			data._clearTime = _clearTime;
		}
	}
}
