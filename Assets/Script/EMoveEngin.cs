using UnityEngine;
using System.Collections;

public class EMoveEngin : MonoBehaviour {
	public GameObject gameMennerger;
	public enum eDIR
	{
		LEFT,
		RIGHT
	}
	
	private string _clipName = "work";
	private Vector3 prevPos; 
	public float speed = 0.001f;
	public float maxMove = 1;
	private bool isMove = true;
	
	private eDIR _dir = eDIR.LEFT;
	
	void Start () 
	{
		tk2dSpriteAnimator animator = GetComponent<tk2dSpriteAnimator>();
		_clipName = animator.DefaultClip.name;
		prevPos = gameObject.transform.localPosition;
		setClip(_clipName);
	}
	
	
	void Update () 
	{	
		if(isMove){
			if(_dir == eDIR.LEFT) {
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - (Time.deltaTime * speed),gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
			}
			else {
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + (Time.deltaTime * speed),gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);//gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + speed,prevPos.y,prevPos.z);
			}
			
			if(gameObject.transform.localPosition.x < prevPos.x - maxMove) 
			{
				_dir = eDIR.RIGHT;
				setFlip(_dir);
			}
			else if(gameObject.transform.localPosition.x > prevPos.x + maxMove) 
			{
				_dir = eDIR.LEFT;
				setFlip(_dir);
			}
		}else{
		}
	}
	
	void setClip(string clipName)
	{
		
		tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
		int id = ani.GetClipIdByName(clipName);
		if(id != -1)
		{
			ani.Play(clipName);
			_clipName = clipName;
		}
	}
	
	void setFlip(eDIR dir)
	{
		_dir = dir;
		tk2dSprite spr = GetComponent<tk2dSprite>();
		if(dir == eDIR.LEFT) spr.scale = new Vector3((Mathf.Abs(spr.scale.x)),spr.scale.y,spr.scale.z);
		else spr.scale = new Vector3((-Mathf.Abs(spr.scale.x)),spr.scale.y,spr.scale.z);
	}
	
	public void isMoving(bool move)
	{
		if(move)
		{
			isMove = true;
		}else{
			isMove = false;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		Player ply = col.gameObject.GetComponent<Player>();
		if(ply != null) ply.enermyHit(gameObject);
	}
}
