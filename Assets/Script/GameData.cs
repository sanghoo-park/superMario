using UnityEngine;
using System.Collections;

public class GameData : MonoBehaviour {
	public int _totalCoins;
	public int _life = 0;
	public int _stageCoins;
	public string _clearTime;
	// Use this for initialization
	void Start () 
	{
		Debug.Log("start data");
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public int life
	{
		get
		{
			return _life;	
		}
		set
		{
			_life = life;
		}
	}
	
	public int stageCoins
	{
		get
		{
			return _stageCoins;	
		}
		set
		{
			_stageCoins = stageCoins;
		}
	}
}
