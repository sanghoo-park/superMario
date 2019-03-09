using UnityEngine;
using System.Collections;

public class EHitEngin : MonoBehaviour {
	public float energy = 1;
	float firstEnergy;
	void Start () {
		firstEnergy = energy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void hit()
	{
		tk2dSpriteAnimator ani = gameObject.GetComponent<tk2dSpriteAnimator>();
		energy--;
		if(energy == 1)
		{
			ani.Play("hit");
			EMoveEngin moveEngin = gameObject.GetComponent<EMoveEngin>();
			moveEngin.isMoving(false);
		}else if(energy == 0)
		{
			ani.Play("die");
			EMoveEngin moveEngin = gameObject.GetComponent<EMoveEngin>();
			moveEngin.isMoving(false);
			energy = firstEnergy;
			GetComponent<EnermyDieMotion>().isDie();
		}
	}
}
