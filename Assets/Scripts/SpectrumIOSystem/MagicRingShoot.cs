using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRingShoot : MonoBehaviour {

	public GameObject ShotBullet;
	public GameObject Wave;

	public void Shooting () {

		StartCoroutine (wait ());

		GameObject Bullet;

		Bullet = ShotBullet;
		//Fire
		GameObject s1 = (GameObject)Instantiate (Bullet, this.transform.position, this.transform.rotation);
		s1.GetComponent<BeamParam> ().SetBeamParam (this.GetComponent<BeamParam> ());

		GameObject wav = (GameObject)Instantiate (Wave, this.transform.position, this.transform.rotation);
		wav.transform.localScale *= 0.25f;
		wav.transform.Rotate (Vector3.left, 90.0f);
		wav.GetComponent<BeamWave> ().col = this.GetComponent<BeamParam> ().BeamColor;

	}

	IEnumerator wait(){
		yield return new WaitForSeconds (1f);
	}
}
