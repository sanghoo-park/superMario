using UnityEngine;
using System.Collections;

public class BlockBump : MonoBehaviour {

	public GameObject blockBreakPrarticle;
	private bool quit = false;
	
	void OnDestroy()
	{
		if(!quit)
		{
			Instantiate(blockBreakPrarticle, transform.position,blockBreakPrarticle.transform.rotation);
			DestroyObject(gameObject);
		}
	}
	
	void OnApplicationQuit()
	{
		quit = true;
	}
}
