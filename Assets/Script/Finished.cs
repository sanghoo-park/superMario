using UnityEngine;
using System.Collections;

public class Finished : MonoBehaviour {
	
	public GameObject casle;
	public GameObject finish;
	public GameObject flag;
	public GameObject casleHitCol;
	bool isUpFlag = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(flag != null)
		{
			if(isUpFlag)
			{
				Vector3 pos = flag.transform.localPosition;
				if(pos.y > 0.75f)
				{
					
				}else{
					flag.transform.localPosition = new Vector3(pos.x,pos.y + (1f * Time.deltaTime),pos.z);
				}
				
			}
		}
	}
	
	public void upFlag()
	{
		isUpFlag = true;
		GameMenneger.instance._flagHit = true;
	}
}
