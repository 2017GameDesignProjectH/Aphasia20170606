using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderScript : MonoBehaviour {

    public float LifeTime;
    public float damageValue;

    // Use this for initialization
    void Start () {
        Invoke("KillYourself", LifeTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillYourself()
    {
        GameObject parent = this.transform.parent.gameObject;
        GameObject.Destroy(parent);
    }

    void OnParticleCollision(GameObject other)
    {
        other.gameObject.SendMessage("Hit", damageValue);
    }
}
