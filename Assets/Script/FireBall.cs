using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {
	
	tk2dSpriteAnimator ani;
	float ballSpeed = .037f;
	float ballGravity = 1.6f;
	float ballMaxH = .3f;
	int changeUp = -1;
	int changeRL = 1;
	bool isHit = false;
	float prevBallPosY;
	void Start () 
	{	
		ani = GetComponent<tk2dSpriteAnimator>();	
		if(GameMenneger.instance._dir == GameMenneger.eDIR.LEFT)
		{
			ballSpeed = ballSpeed * -1;
		}else{
			ballSpeed = ballSpeed * 1;
		}
		StartCoroutine("destroyFireBall",3);
	}
	
	
	void Update () 
	{
		if(changeUp == 1) if(transform.localPosition.y-ballMaxH > prevBallPosY) changeUp = -1;
		if(!isHit) transform.localPosition = new Vector3(transform.localPosition.x+(changeRL * ballSpeed),transform.localPosition.y + (changeUp * Time.deltaTime * ballGravity),0);
	}
	
	IEnumerator destroyFireBall(float sec)
	{
		yield return new WaitForSeconds(sec);
		Destroy(gameObject);
	}
	
	
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "LEND" || col.gameObject.tag == "BLOCK_BREAKABLE" 
			|| col.gameObject.tag == "ENERMY1" || col.gameObject.tag == "ENERMY2" || col.gameObject.tag == "ENERMY3"
			|| col.gameObject.tag == "ITEMBOX" || col.gameObject.tag == "PIPE"){
			float objH = col.gameObject.transform.localScale.y/2f;
			if(transform.localPosition.y > col.gameObject.transform.localPosition.y + objH)
			{
				prevBallPosY = transform.localPosition.y;
				changeUp = 1;
			}else if(transform.localPosition.y < col.gameObject.transform.localPosition.y - objH)
			{
				changeUp = -1;
			}else if(transform.localPosition.y > col.gameObject.transform.localPosition.y - objH &&
				transform.localPosition.y < col.gameObject.transform.localPosition.y + objH)
			{
				isHit = true;
				ani.Play("hit");
				StartCoroutine("destroyFireBall",.2f);
			}
			
			if(col.gameObject.tag == "ENERMY1" || col.gameObject.tag == "ENERMY2" || col.gameObject.tag == "ENERMY3")
			{
				isHit = true;
				EHitEngin hitEnermy = col.gameObject.GetComponent<EHitEngin>();
				if(hitEnermy.energy == 2) hitEnermy.hit();
				hitEnermy.hit();
				ani.Play("hit");
				StartCoroutine("destroyFireBall",.2f);
			}
		}
	}
}
