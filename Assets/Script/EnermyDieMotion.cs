using UnityEngine;
using System.Collections;

public class EnermyDieMotion : MonoBehaviour {
	private bool _isDie = false;
	bool _isDown = false;
	private float _gravity = 1f;
	private tk2dSprite sp;
	// Use this for initialization
	void Start () 
	{
		sp = GetComponent<tk2dSprite>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_isDie)
		{
			//sp.color = new Color(1,1,1,sp.color.a + .1f * Mathf.Ceil(Time.deltaTime));
			//sp.color = new Color(1,1,1,sp.color.a - (.1f * Mathf.Ceil(Time.deltaTime)/2));
			
			gameObject.collider.enabled = false;
			StartCoroutine("objDown",.3f);
			StartCoroutine("objDelete",1f);
			Vector3 localPos = transform.localPosition;
			transform.eulerAngles = new Vector3(180,0,0);
			if(_isDown)
			{
				transform.localPosition = new Vector3(localPos.x,localPos.y+(-_gravity*Time.deltaTime),0);
			}
			else
			{
				transform.localPosition = new Vector3(localPos.x,localPos.y+(_gravity*Time.deltaTime),0);
			}
		}
	}
	
	IEnumerator objDown(float sec)
	{
		yield return new WaitForSeconds(sec);
		_isDown = true;
	}
	
	IEnumerator objDelete(float sec)
	{
		yield return new WaitForSeconds(sec);
		Destroy(gameObject);
	}
	
	public void isDie()
	{
		_isDie = true;
	}
}
