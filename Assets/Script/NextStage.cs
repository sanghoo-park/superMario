using UnityEngine;
using System.Collections;

public class NextStage : MonoBehaviour {
	GameObject _finishedUI;
	GameObject _reStartUI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onNextStage()
	{
	 	GameObject[] objects = (GameObject[])FindSceneObjectsOfType(typeof(GameObject));
        foreach (GameObject o in objects){
			if(o.name.Contains("block")) DestroyObject(o);
		}
		
		if(GameObject.FindWithTag("FINISHUI") != null){
			_finishedUI = GameObject.FindWithTag("FINISHUI");
		}
		GameMenneger.instance.coins = 0;
		_finishedUI.SetActive(false);
		if(Application.loadedLevel == Application.levelCount-1)
		{
			Destroy(GameObject.FindWithTag("GAMEDATA"));
			Application.LoadLevel(0);
		}else
		{
			Application.LoadLevel(Application.loadedLevel+1);
		}
	}
	
	void onReStart()
	{
	 	GameObject[] objects = (GameObject[])FindSceneObjectsOfType(typeof(GameObject));
        foreach (GameObject o in objects){
			if(o.name.Contains("block")) DestroyObject(o);
		}
		
		if(GameObject.FindWithTag("RESTARTUI") != null){
			_reStartUI = GameObject.FindWithTag("RESTARTUI");
		}
		GameMenneger.instance.coins = 0;
		_reStartUI.SetActive(false);
		Destroy(GameObject.FindWithTag("GAMEDATA"));
		Application.LoadLevel(0);
	}
}
