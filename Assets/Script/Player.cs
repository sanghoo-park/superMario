using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public enum eDIR{
		LEFT,
		RIGHT
	}
	
	public float gravity = 6f;
	public float jumpSpeed = 2.5f;
	public GameObject gameMenneger;
	public GameObject fireBall;
	GameObject finishObject;
	
	private Vector3 velocity_ = Vector3.zero;
	private float accel_ = 1f;
	private string clipName_;
	private eDIR dir_ = eDIR.RIGHT;
	
	private CharacterController chcon;
	private bool superMode = false;
	private bool isSuper = false;
	private bool isFire = false;
	private bool isDie = false;
	private bool isBounce = false;
	private bool isRiversBounce = false;
	private bool blockHitFlag = true;
	private bool isSuperModeColor = false;
	private bool isPlayerDelayDieMotion = false;
	bool isFinish = false;
	bool isPlayerDown = false;
	bool isFireBallDelay = true;
	float fireDelayNum = .3f; // fireBall reLoad Time
	
	tk2dSprite sp;
	void Start () 
	{
		tk2dSpriteAnimator animator = GetComponent<tk2dSpriteAnimator>();
		chcon = GetComponent<CharacterController>();
		clipName_ = animator.DefaultClip.name;
		sp = GetComponent<tk2dSprite>();
	}
	
	void Update () 
	{
		CharacterController controller = GetComponent<CharacterController>();
		string clipName;
		if(isSuper){
			clipName = "Sidle";
			if(isFire) clipName = "Fidle";
		}
		else clipName = "idle";
		bool isLand = controller.isGrounded;
		eDIR dir = dir_;
	
		bool move = false;
		FollowCamera2D fc = GetComponent<FollowCamera2D>();
		if(velocity_.x < 0f)
		{
			Vector3 newPos = transform.position;
			if(newPos.x < -1.4f) newPos.x = -1.4f;
			transform.position = newPos;
			dir = eDIR.LEFT;
			move = true;
		}else if(velocity_.x > 0f)
		{
			Vector3 newPos = transform.position;
			if(fc && newPos.x > 1.4f + fc.scrollWidth) newPos.x = 1.4f + fc.scrollWidth;
			transform.position = newPos;
			
			dir = eDIR.RIGHT;
			move = true;
		}
		if(!isFinish) //---------------------------------------------------------------- STAGE UNFINISHED;
		{
			if(superMode)
			{
				if(sp.color.a >= 1){
					isSuperModeColor = false;					
				}else if(sp.color.a <= .5f){
					isSuperModeColor = true;
				}				
				if(isSuperModeColor) sp.color = new Color(1,1,1,sp.color.a + .1f * Mathf.Ceil(Time.deltaTime));
				else sp.color = new Color(1,1,1,sp.color.a - .1f * Mathf.Ceil(Time.deltaTime));
			}else{
				isSuperModeColor = false;
				sp.color = new Color(1,1,1,1);
			}
			if(!isDie) velocity_.x = Input.GetAxis("Horizontal") * accel_;
			if(!isLand)
			{	
				velocity_.y -= gravity * Time.deltaTime;
			}
			
			if(isLand){
				if(Input.GetKeyDown(KeyCode.X)) velocity_.y = jumpSpeed + accel_ * 0.5f;
				blockHitFlag = true;
			}else{
				if(isSuper){
					clipName = "Sjump";
					if(isFire) clipName = "Fjump";
				}
				else clipName = "jump";
				
				if(Input.GetKeyUp(KeyCode.X)) velocity_.y -= 1f;
			}
			if(isFire) //-----------------------------------------------------fire!!!!!!
			{
				if(Input.GetKeyDown(KeyCode.Z))
				{			
					if(isFireBallDelay)
					{
						float posX;
						if(dir_ == eDIR.LEFT)
						{
							posX = transform.localPosition.x - 0.18f;
						}
						else
						{
							posX = transform.localPosition.x + 0.18f;
						}
						Instantiate(fireBall,new Vector3(posX,transform.localPosition.y + 0.13f,transform.localPosition.z),Quaternion.identity);
						isFireBallDelay = false;
						StartCoroutine("onFire",fireDelayNum);
					}
				}
			}
			
			if(move)
			{
				if(isLand)
				{
					if(Input.GetKey(KeyCode.Z))
					{
						if(isSuper){
							clipName = "Srun";
							if(isFire) clipName = "Frun";
						}
						else clipName = "run";
						
						accel_ += 0.03f;
						if(accel_ > 2.5f) accel_ = 2.5f;
					}else{
						if(isSuper) {
							clipName = "Swalk";
							if(isFire) clipName = "Fwalk";
						}
						
						else clipName = "walk";
						accel_ = 1f;
					}
				}
			}
			if(!isDie) controller.Move(velocity_ * Time.deltaTime);
			if(dir != dir_)
			{
				SetFlip(dir);
				accel_ = 1f;
			}
			
			if(!isDie) if(clipName != clipName_) SetClip(clipName);
			
		}else{ //---------------------------------------------------------------- STAGE FINISHED;
			if(!isLand)
			{	
				velocity_.y -= gravity * Time.deltaTime;
			}else{
				if(isSuper){
					clipName = "Swalk";
					if(isFire) clipName = "Fwalk";
				}
				else clipName = "walk";
				SetClip(clipName);
				accel_ = .5f;
				velocity_.x = accel_;
				finishObject.collider.enabled = false;
			}
			
			if(!GameMenneger.instance._stageComplete){
				controller.Move(velocity_ * Time.deltaTime);
			}else{
				if(isSuper){
					clipName = "Sidle";
					if(isFire) clipName = "Fidle";
				}
				else clipName = "idle";
				SetClip(clipName);
				if(sp.color.a < 0)
				{
					//Debug.Log("next Stage");
				}else{
					sp.color = new Color(1,1,1,sp.color.a - .05f * Mathf.Ceil(Time.deltaTime));
				}
			}
		}	
		
		if(isBounce)
		{
			isBounce = false;
			velocity_.y = 0f;
			velocity_.y -= 1f;
		}
		
		if(isRiversBounce)
		{
			isRiversBounce = false;
			velocity_.y = 0f;
			velocity_.y += 1.5f;
		}
		
		if(isDie) 
		{
			StartCoroutine("playerDelayDieMotion",.3f);
			if(isPlayerDelayDieMotion) if(!isPlayerDown){
				Vector3 pos = transform.localPosition;
				pos.y += gravity/2 * Time.deltaTime;
				transform.localPosition = pos;
			}
		}
		
		if(isPlayerDown) //==========================player dir motion & enermy hit motion
		{
			isPlayerDelayDieMotion = false;
			Vector3 pos = transform.localPosition;
			pos.y -= gravity * Time.deltaTime;
			transform.localPosition = pos;
			GameMenneger.instance.setDie(true);
		}else if(transform.position.y < -0.9f){
			chcon.collider.enabled = false;
			isDie = true;
			isRiversBounce = true;
			tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
			ani.Play("die");
			controller.collider.isTrigger = true;
			StartCoroutine("playerDown",.5f);
		}
	}
	
	
	IEnumerator playerDelayDieMotion(float sec)
	{
		yield return new WaitForSeconds(sec);
		isPlayerDelayDieMotion = true;
	}
	
	IEnumerator onFire(float sec)
	{
		yield return new WaitForSeconds(sec);
		isFireBallDelay = true;
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if(!superMode){
			if(hit.collider.tag == "TOADSTOOL")
			{
				Debug.Log("eat toad");
				isSuper = true;
				chcon.height = 2;
				Vector3 pos = transform.localPosition;
				pos.y += 0.1f;
				transform.localPosition = pos;
				Destroy(hit.collider.gameObject);
			}else if(hit.collider.tag == "FIREFLOWER")
			{
				Debug.Log("eat fire!!!");
				isSuper = true;
				isFire = true;
				chcon.height = 2;
				Vector3 pos = transform.localPosition;
				pos.y += 0.1f;
				transform.localPosition = pos;
				Destroy(hit.collider.gameObject);
			}		
		}
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if(!isDie)
		{
			CharacterController controller = GetComponent<CharacterController>();
			if(blockHitFlag && hit.collider.tag == "BLOCK_BREAKABLE")
			{
				if(controller.collisionFlags == CollisionFlags.Above)
				{
					if(!isBounce && controller.velocity.y > 0f)
					{
						DestroyObject(hit.collider.gameObject);
						isBounce = true;
						blockHitFlag = false;
					}
				}
			}
			
			if(hit.collider.tag == "FINISH")
			{
				finishObject = hit.collider.gameObject;
				Finished fo = finishObject.transform.parent.GetComponent<Finished>();
				fo.upFlag();
				isFinish = true;
			}

			if(hit.collider.tag == "ITEMBOX")
			{
				if(controller.collisionFlags == CollisionFlags.Above)
				{
					if(!isBounce && controller.velocity.y > 0f)
					{
						isBounce = true;
						ItemBox ibox = hit.collider.gameObject.GetComponent<ItemBox>();
						ibox.hit();
					}
				}
			}
			
			if(!superMode){
				if(hit.collider.tag == "ENERMY1")
				{
					if(gameObject.transform.localPosition.y - gameObject.transform.localScale.y / 2 > hit.collider.gameObject.transform.localPosition.y)
					{
						isRiversBounce = true;
						EHitEngin hitEnermy = hit.collider.gameObject.GetComponent<EHitEngin>();
						hitEnermy.hit();
					}else{
						playerDie(hit.collider.gameObject);
					}
					
				}else if(hit.collider.tag == "ENERMY2")
				{
					if(gameObject.transform.localPosition.y - gameObject.transform.localScale.y / 2 > hit.collider.gameObject.transform.localPosition.y)
					{
						isRiversBounce = true;
						EHitEngin hitEnermy = hit.collider.gameObject.GetComponent<EHitEngin>();
						hitEnermy.hit();
					}else{
						playerDie(hit.collider.gameObject);
					}
				}else if(hit.collider.tag == "ENERMY3")
				{
					playerDie(hit.collider.gameObject);
				}
				
			}
		}
	}
	
	void playerDie(GameObject hitObj)
	{
		BoxCollider box = GetComponent<BoxCollider>();
		CharacterController controller = GetComponent<CharacterController>();
		
		if(isSuper){
			if( null != box )
			{
				box.enabled = false;
			}
			controller.collider.isTrigger = true;
			superMode = true;
			isSuper = false;
			isFire = false;
			chcon.height = 1.04F;
			StartCoroutine("unSuper",2f);
			
		}else{
			EMoveEngin mov = hitObj.GetComponent<EMoveEngin>();
			mov.isMoving(false);
			chcon.collider.enabled = false;
			isDie = true;
			isRiversBounce = true;
			tk2dSpriteAnimator ani = GetComponent<tk2dSpriteAnimator>();
			ani.Play("die");
			controller.collider.isTrigger = true;
			StartCoroutine("playerDown",.5f);
			
			if( null != box )
			{
				box.enabled = false;
			}
			
		}
	}
	
	IEnumerator playerDown(float seconde)
	{
		yield return new WaitForSeconds(seconde);
		chcon.collider.enabled = false;
		isPlayerDown = true;
	}
	
	IEnumerator unSuper(float seconde)
	{
		yield return new WaitForSeconds(seconde);
		BoxCollider box = GetComponent<BoxCollider>();
		CharacterController controller = GetComponent<CharacterController>();
		if( null != box )
		{
			box.enabled = true;
		}
		controller.collider.isTrigger = false;
		
		Debug.Log("re die");
		superMode = false;
	}
	
	void SetClip(string clipName)
	{
		tk2dSpriteAnimator animator = GetComponent<tk2dSpriteAnimator>();
		int id = animator.GetClipIdByName(clipName);
		
		if(id != -1)
		{
			animator.Play(clipName);
			clipName_ = clipName;
		}
	}
	
	void SetFlip(eDIR dir)
	{
		dir_ = dir;
		tk2dSprite spr = GetComponent<tk2dSprite>();
		
		if(dir == eDIR.LEFT){
			spr.scale = new Vector3(-Mathf.Abs(spr.scale.x), spr.scale.y, spr.scale.z);
			GameMenneger.instance._dir = GameMenneger.eDIR.LEFT;
		}
		else if(dir == eDIR.RIGHT){
			spr.scale = new Vector3(Mathf.Abs(spr.scale.x), spr.scale.y, spr.scale.z);
			GameMenneger.instance._dir = GameMenneger.eDIR.RIGHT;
		}
	}
	
	public void enermyHit(GameObject go)
	{
		playerDie(go);
	}
}
