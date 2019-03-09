using UnityEngine;
using System.Collections;

public class PipeEnermyMove : MonoBehaviour 
{
	public enum eDIR
	{
		UP,
		DOWN,
		WAIT
	}
	public float downSpeed = 0.5f;
	eDIR _dir = eDIR.DOWN;
	Vector3 pos;
	
	void Start () 
	{
		pos = gameObject.transform.localPosition;
	}
	
	void Update () 
	{
		if(_dir == eDIR.DOWN)
		{
			if(pos.y - gameObject.transform.localPosition.y  > gameObject.transform.localScale.y){
				_dir = eDIR.UP;
			}else{
				gameObject.transform.localPosition = new Vector3(pos.x,gameObject.transform.localPosition.y - (downSpeed * Time.deltaTime),pos.z);
			}
			
		}else if(_dir == eDIR.UP){
			if(pos.y - gameObject.transform.localPosition.y  < -gameObject.transform.localScale.y){
				_dir = eDIR.DOWN;
			}else{
				gameObject.transform.localPosition = new Vector3(pos.x,gameObject.transform.localPosition.y + (downSpeed * Time.deltaTime),pos.z);
			}
		}else if(_dir == eDIR.WAIT){
			
		}
	}
}
