using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class ItemBox : MonoBehaviour {
	public enum eDIR{TOADESTOOL,COIN, FIRE}
	public eDIR dir;
	
	public GameObject toadeStool;
	public GameObject coin;
	public GameObject fireFlower;
	GameObject activeObj;
	GameObject item;
	
	bool boxBounce = false;
	bool boxBounceUp = true;
	bool activeItem = false;
	bool isEmpty = false;
	bool isUp = true;
	
	Vector3 prevPos;
	Vector3 prevBoxPos;
	
	public int coinNum = 5;
	// Use this for initialization
	void Start () 
	{
		prevBoxPos = transform.localPosition;
		if(dir == eDIR.COIN)
		{
			activeObj = coin;
		}else if(dir == eDIR.TOADESTOOL)
		{
			activeObj = toadeStool;
			coinNum = 1;
		}else if(dir == eDIR.FIRE)
		{
			activeObj = fireFlower;
			coinNum = 1;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		if(dir == eDIR.TOADESTOOL)
		{
			if(!isEmpty)
			{
				if(activeItem)
				{
					activeItem = false;
					item = Instantiate(activeObj,transform.localPosition,transform.rotation) as GameObject;
					prevPos = item.transform.localPosition;
					tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
					ani.Play("empty");
					coinNum--;
				}
			}
			
			if(item != null)
			{
				Vector3 itemPos = item.transform.localPosition;
				
				if(itemPos.y > prevPos.y + item.transform.localScale.y*1.5) isUp = false;
				if(isUp)
				{
					item.transform.localPosition = new Vector3(itemPos.x,itemPos.y + 1f*Time.deltaTime,.01f);
				}else{
					if(itemPos.y - transform.localScale.y < prevPos.y){
						item.transform.localPosition = new Vector3(prevPos.x,prevPos.y+transform.localScale.y,prevPos.z);
					}else{
						if(item.rigidbody == null) item.AddComponent<Rigidbody>();
					}					
				}
			}
			
		}
		
		if(dir == eDIR.FIRE)
		{
			if(!isEmpty)
			{
				if(activeItem)
				{
					activeItem = false;
					item = Instantiate(activeObj,transform.localPosition,transform.rotation) as GameObject;
					prevPos = item.transform.localPosition;
					tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
					ani.Play("empty");
					coinNum--;
				}
			}
			
			if(item != null)
			{
				Vector3 itemPos = item.transform.localPosition;
				
				if(itemPos.y > prevPos.y + item.transform.localScale.y*1.5) isUp = false;
				if(isUp)
				{
					item.transform.localPosition = new Vector3(itemPos.x,itemPos.y + 1f*Time.deltaTime,.01f);
				}else{
					if(itemPos.y - transform.localScale.y < prevPos.y){
						item.transform.localPosition = new Vector3(prevPos.x,prevPos.y+transform.localScale.y,prevPos.z);
					}else{
						if(item.rigidbody == null) item.AddComponent<Rigidbody>();
					}					
				}
			}
			
		}
		
		if(dir == eDIR.COIN)
		{
			if(!isEmpty)
			{
				if(activeItem)
				{
					activeItem = false;
					coinNum--;
					item = Instantiate (activeObj,transform.localPosition,transform.rotation) as GameObject;
					item.transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,0.1f);
					if(coinNum == 0)
					{
						
						activeItem = false;
						tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
						ani.Play("empty");
					}
				}
			}
		}
		
		if(!isEmpty)
		{
			if(boxBounce)
			{
				Vector3 boxPos = transform.localPosition;
				if(prevBoxPos.y + (transform.localScale.y / 2.0f) < transform.localPosition.y)
				{
					boxBounceUp = false;	
				}
				
				if(boxBounceUp)
				{
					transform.localPosition = new Vector3(boxPos.x,boxPos.y + (5 * Time.deltaTime),boxPos.z);
				}
				else
				{
					transform.localPosition = new Vector3(boxPos.x,boxPos.y - (5* Time.deltaTime),boxPos.z);
					if(transform.localPosition.y < prevBoxPos.y){
						transform.localPosition = prevBoxPos;
						boxBounce = false;
						boxBounceUp = true;
						if(coinNum == 0)
						{
							isEmpty = true;
						}
					}
				}
			}
		}
		
	}
	
	public void hit()
	{
		activeItem = true;
		boxBounce = true;
		if(isEmpty)
		{
			
		}
		else
		{
			
		}
	}
}
