using UnityEngine;
using System.Collections;

public class FollowCamera2D : MonoBehaviour {
	public float scrollWidth = 10f;
	public float scrollHeight = 2f;
	
	private Vector3 oldCameraPos_;
	
	void Start () 
	{
		oldCameraPos_ = Camera.main.transform.position;
	}
	
	void Update () 
	{
		Vector3 scrollPos = new Vector3(transform.position.x,transform.position.y - 0.5f, 0f);
		if(scrollPos.x < 0f) scrollPos.x = 0f;
		else if(scrollPos.x > scrollWidth) scrollPos.x = scrollWidth;
		
		if(scrollPos.y < 0f) scrollPos.y = 0f;
		else if(scrollPos.y > scrollHeight) scrollPos.y = scrollHeight;
		Camera.main.transform.position = oldCameraPos_ + scrollPos;
	}
}
