using UnityEngine;
using System.Collections;

public class CasleHit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log(col.tag);
		if(col.tag == "PLAYER"){
			GameMenneger.instance._stageComplete = true;
			GameMenneger.instance.finisedUI.SetActive(true);
		}
	}
}
