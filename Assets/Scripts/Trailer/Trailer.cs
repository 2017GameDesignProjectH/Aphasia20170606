using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Trailer : MonoBehaviour {

	public GameObject camera;
	public GameObject DarkDisplay;
	public GameObject piano;
	public List<TrailerNode> node;
	public Image BlackCover;


	public GameObject particle1;
	public GameObject particle2;

	public float timer;

	public int nodeCount;

	// Use this for initialization
	void Start () {
		timer = 0f;
		camera.transform.position = node [0].position1;
		//camera.transform.LookAt (piano.transform.position);
	}

	// Update is called once per frame
	void Update () {
		//camera.transform.LookAt (piano.transform.position);
		if(nodeCount>=node.Count){
			return;
		}
		node [nodeCount].stayTime -= Time.deltaTime;
		if (!node[nodeCount].StartMove && node [nodeCount].stayTime <= 0) {
			node[nodeCount].StartMove = true;
			if (nodeCount != 3) {
				DOTween.To (() => BlackCover.color, (x) => BlackCover.color = x, new Color (0, 0, 0, 1), 1f).SetDelay (node [nodeCount].deltaTime - 1);
			}
			camera.transform.DOMove (node [nodeCount].position2, node [nodeCount].deltaTime)
				.OnComplete(()=>{
					nodeCount++;
					if (nodeCount == 3){
						particle1.SetActive(true);
						particle2.SetActive(true);
						camera.transform.position = node[nodeCount].position1;
						DOTween.To (()=> BlackCover.color, (x)=>BlackCover.color=x, new Color(0,0,0,0),0.1f).SetDelay(1f);
					}
					else{
						camera.transform.position = node[nodeCount].position1;
						DOTween.To (()=> BlackCover.color, (x)=>BlackCover.color=x, new Color(0,0,0,0),0.1f);
					}
				});
		}
	}
}

[System.Serializable]
public class TrailerNode{
	public Vector3 position1;
	public Vector3 position2;
	public bool StartMove = false;
	public bool dark;
	public float stayTime;
	public float deltaTime;
}
