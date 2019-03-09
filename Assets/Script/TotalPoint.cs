using UnityEngine;
using System.Collections;

public class TotalPoint : MonoBehaviour {
	UILabel label;
	int total;
	int getCoin;
	int i = 0;
	GameData data;
	// Use this for initialization
	void Start () 
	{
		label = GetComponent<UILabel>();
		data = GameObject.FindWithTag("GAMEDATA").GetComponent<GameData>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameMenneger.instance._stageComplete || GameMenneger.instance._reStart)
		{
			getCoin = GameMenneger.instance.coins;
			if(i != getCoin){
				i++;
				total = GameMenneger.instance.totalCoinNum + i;
				label.text = "GET COIN = " + GameMenneger.instance.coins + "\n" + "TOTAL COIN = " + total + "\n" + "TIME = " + data._clearTime;	
			}else{
				GameMenneger.instance.totalCoinNum = total;
				if(GameObject.FindWithTag("GAMEDATA") != null){
					data._totalCoins = total;
				}
			}	
		}
	}
}
