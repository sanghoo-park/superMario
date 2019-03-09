using UnityEngine;
using System.Collections;

public class CoinMotion : MonoBehaviour {
	public enum eDIR{
		UP,
		DOWN
	}
	
	public float rotationSpeed = 200f;
	public float upSpeed = 20f;
	bool eat = false;
	float alphaNum = 1f;
	
	eDIR _dir = eDIR.UP;
	
	float posY;
	void Start()
	{
		posY = transform.localPosition.y;
	}
	
	void Update () 
	{
		transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
		if(eat)
		{
			tk2dSprite sp = GetComponent<tk2dSprite>();
			sp.color = new Color(1,1,1,alphaNum);
			alphaNum -= 0.025f;
			if(posY+0.25 < transform.localPosition.y){
				_dir = eDIR.DOWN;
			}
			
			if(posY-0.15 > transform.localPosition.y){
				Destroy(gameObject);
				GameMenneger.instance.getCoins();
			}
			
			if(_dir == eDIR.UP){
				transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y + (upSpeed/2 * Time.deltaTime),transform.localPosition.z);
			}else if(_dir == eDIR.DOWN){
				transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y - (upSpeed * Time.deltaTime),transform.localPosition.z);	
			}
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "PLAYER")
		{
			eat = true;
		}
	}
}
