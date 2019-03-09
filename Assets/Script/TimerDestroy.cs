using UnityEngine;
using System.Collections;

public class TimerDestroy : MonoBehaviour {
	public float timeOut = 1f;
	public float lastTime;
	
	void Start () 
	{
		lastTime = Time.time;
	}
	
	void Update () 
	{
		if(Time.time > timeOut + lastTime) DestroyObject(gameObject);
	}
}
