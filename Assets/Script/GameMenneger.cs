using UnityEngine;
using System.Collections;

public class GameMenneger : MonoBehaviour {
	public enum eDIR{
		LEFT,RIGHT	
	}
	
	public eDIR _dir = eDIR.RIGHT;
	public bool _die = false;
	public int _life;
	public bool _stageComplete = false;
	public bool _flagHit = false;
	public bool _reStart = false;
	public int coins = 0;
	public int totalCoins = 0;
	private static GameMenneger singleton = null;
	GameObject _finishedUI;
	GameObject _reStartUI;
	public static int _totalCoins = 0;
	GameObject gameData;
	GameData data;
	
	void Start () 
	{
		
		if(GameObject.FindWithTag("FINISHUI") != null){
			_finishedUI = GameObject.FindWithTag("FINISHUI");
		}
		
		if(GameObject.FindWithTag("RESTARTUI") != null){
			_reStartUI = GameObject.FindWithTag("RESTARTUI");
		}
		
		if(GameObject.FindWithTag("GAMEDATA") != null){
			gameData = GameObject.FindWithTag("GAMEDATA");
		}
		
		_finishedUI.SetActive(false);
		_reStartUI.SetActive(false);
		coins = 0;
		_totalCoins = gameData.GetComponent<GameData>()._totalCoins;
		 data = gameData.GetComponent<GameData>();
		_life = data.life;
	}
	
	void resultUI(bool isShow)
	{
		if(isShow)
		{
			_finishedUI.SetActive(true);
		}else
		{
			_finishedUI.SetActive(false);
		}
	}
	
	void reStartUI(bool isShow)
	{
		if(isShow)
		{
			_reStartUI.SetActive(true);
		}
		else
		{
			_reStartUI.SetActive(false);
		}
	}
	
	void Update () 
	{
	
	}
	
	public static GameMenneger instance
	{
		get{
			if(singleton == null)
			{
				GameObject obj = new GameObject();
				obj.AddComponent<GameMenneger>();
				singleton = obj.GetComponent<GameMenneger>();
			}
			return singleton;
		}
		
		private set{
		}
	}
	
	public GameObject finisedUI
	{
		get
		{
			return _finishedUI;
		}
	}
	
	public eDIR dir
	{
		get
		{
			return _dir;
		}
		set
		{
			_dir = dir;
		}
	}
	
	public int totalCoinNum
	{
		get
		{
			return _totalCoins;
		}
		set
		{
			_totalCoins = totalCoinNum;
		}
	}
	
	public void getCoins()
	{
		coins += 1;
		data._stageCoins = coins;
	}
	
	public void setDie(bool die)
	{
		_die = die;
		StartCoroutine("reStartStage",1f);
	}
	
	IEnumerator reStartStage(float sec)
	{
		yield return new WaitForSeconds(sec);
		_life -= 1;
		if(_life <= -1){
			Destroy(gameData);
			reStartUI(true);
			_reStart = true;
			_die = false;
		}else {
			GameObject[] objects = (GameObject[])FindSceneObjectsOfType(typeof(GameObject));
	        foreach (GameObject o in objects){
				if(o.name.Contains("block")) DestroyObject(o);
			}
			data._life = _life;
			data._stageCoins = 0;
			_die = false;
			int level =	Application.loadedLevel;
			Application.LoadLevel(level);			
		}
	}
}
