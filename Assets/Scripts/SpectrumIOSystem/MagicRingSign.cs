using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRingSign : MonoBehaviour {

	public MagicRingSystem magicRingSystem;

	public PlayerSkill sign;

	public float timer;
	public float judgeBadTime = 1.2f;
	public float judgeGoodTime = 0.3f;
	public bool judge = false;


	// Update is called once per frame
	void FixedUpdate () {
		this.transform.localPosition = GetPosition ();
		timer = timer - Time.fixedDeltaTime;
		if (timer < judgeBadTime && !judge) {
			judge = true;
			magicRingSystem.magicRingSignQueue.Enqueue (this.gameObject);
		}
		if (timer < -judgeGoodTime) {
			magicRingSystem.GunBreak ();
            magicRingSystem.magicRingSignQueue.Dequeue();
			Destroy (this.gameObject);
		}
	}

	public void SetTimer(float iniTime){
		timer = iniTime;
		this.transform.localPosition = GetPosition ();
	}

	public Vector3 GetPosition(){
		float x = timer * 9f;
		float y = timer * timer * timer * 1.25f;
		return new Vector3 (x, y, 0);
	}

	public bool Judge(){
		if (timer <= judgeGoodTime && timer > -judgeGoodTime) {
			return true;
		} else {
			return false;
		}
	}
}
