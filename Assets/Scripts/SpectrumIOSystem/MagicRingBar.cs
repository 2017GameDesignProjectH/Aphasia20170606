using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicRingBar : MonoBehaviour {

	public RectTransform bar;

	public float value = 0f;
	public float max_value = 1f;

	public float basicOffset = 315f;

	void Start(){
		bar = this.GetComponent<RectTransform> ();
	}

	// Update is called once per frame
	void Update () {
		float rate = value / max_value;
		bar.offsetMin = new Vector2 (basicOffset * (rate - 1f), 0f);
		bar.offsetMax = new Vector2 (basicOffset * (rate - 1f), 0f);
	}

	public void SetValue(int MaxValue, int Value){
		max_value = MaxValue;
		value = Value;
	}
}
