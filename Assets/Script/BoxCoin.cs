using UnityEngine;
using System.Collections;

public class BoxCoin : MonoBehaviour {
	float upSpeed = 2f;
	Vector3 pos;
	float rotationSpeed = 200f;
	bool moveUp = true;
	void Start () 
	{
		pos = transform.localPosition;
	}

	void Update () 
	{
		transform.Rotate(0,rotationSpeed * Time.deltaTime,0);
		if(transform.localPosition.y > transform.localScale.y * 2f)
		{
			moveUp = false;
		}else{
			
		}
		if(moveUp)transform.localPosition = new Vector3(pos.x,transform.localPosition.y + (upSpeed*Time.deltaTime),pos.z);
		else 
		{
			transform.localPosition = new Vector3(pos.x,transform.localPosition.y - (1*Time.deltaTime),pos.z);
			tk2dSprite ani = GetComponent<tk2dSprite>();
			ani.color = new Vector4(1,1,1,ani.color.a -(7f*Time.deltaTime));
			if(ani.color.a < 0)
			{
				Destroy(gameObject);
				GameMenneger.instance.getCoins();
			}
		}
	}
}
