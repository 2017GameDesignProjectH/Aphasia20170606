using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicRingSystem : MonoBehaviour {

	public PlayerSkill[] playerSkills;
	public AudioSource stageMusic;

	public int skillNum;
	public float timeOffset;

	public GameObject basicRing;

	public GameObject chargeBar;

	public Dictionary<int, MagicRingWave> magicRingWave = new Dictionary<int, MagicRingWave> ();

	public Queue<GameObject> magicRingSignQueue = new Queue<GameObject> ();

	public MagicRingShoot magicRingShoot;

	// Update is called once per frame
	void FixedUpdate () {
		basicRing.transform.Rotate (new Vector3 (0, 10, 0) * Time.fixedDeltaTime);
	}

	public void SettingPlayerSkills(StageInfo stageInfo){
		playerSkills = stageInfo.PlayerSkills;
		skillNum = 0;
	}

	public void SettingStageMusic(AudioSource music){
		this.stageMusic = music;
	}

    public int count = 0;

	void Update(){
        /*if (count % 100 == 0)
        {
            magicRingShoot.Shooting();
        }
        count = count + 1;*/
		if (skillNum < playerSkills.Length && playerSkills [skillNum].Time <= stageMusic.time + timeOffset) {
			if (playerSkills [skillNum].Tag == "StartCharge" || playerSkills [skillNum].Tag == "OneShoot") {
				try{
					Debug.Log(magicRingWave [playerSkills [skillNum].Wave]);
				}catch(KeyNotFoundException){
					magicRingWave [playerSkills [skillNum].Wave] = new MagicRingWave (playerSkills [skillNum].Wave, playerSkills [skillNum].ChargeNum, chargeBar);
					//Debug.Log ("Yes");
				}
			}
			CreateSign (playerSkills [skillNum]);
			skillNum = skillNum + 1;
		}

		if (magicRingSignQueue.Count > 0) {
			Debug.Log (magicRingSignQueue.Count);
			if (Input.GetKeyDown (KeyCode.JoystickButton0)) {
				GameObject obj = magicRingSignQueue.Dequeue ();
				if (obj.tag == "Square" && obj.GetComponent<MagicRingSign> ().Judge ()) {
					PlayerSkill sign = obj.GetComponent<MagicRingSign> ().sign;
					if(magicRingWave [sign.Wave].Charge(sign.Tag))
                    {
                        magicRingShoot.Shooting();
                    }
				} else {
					GunBreak ();
				}
                Destroy(obj);
            }
			else if (Input.GetKeyDown (KeyCode.JoystickButton1)) {
				GameObject obj = magicRingSignQueue.Dequeue ();
				if (obj.tag == "Cross" && obj.GetComponent<MagicRingSign> ().Judge ()) {
					PlayerSkill sign = obj.GetComponent<MagicRingSign> ().sign;
                    if (magicRingWave[sign.Wave].Charge(sign.Tag))
                    {
                        magicRingShoot.Shooting();
                    }
				} else {
					GunBreak ();
				}
                Destroy(obj);
            }
			else if (Input.GetKeyDown (KeyCode.JoystickButton2)) {
				GameObject obj = magicRingSignQueue.Dequeue ();
				if (obj.tag == "Circle" && obj.GetComponent<MagicRingSign> ().Judge ()) {
					PlayerSkill sign = obj.GetComponent<MagicRingSign> ().sign;
                    if (magicRingWave[sign.Wave].Charge(sign.Tag))
                    {
                        magicRingShoot.Shooting();
                    }
				} else {
					GunBreak ();
				}
                Destroy(obj);
            }
			else if (Input.GetKeyDown (KeyCode.JoystickButton3)) {
				GameObject obj = magicRingSignQueue.Dequeue ();
				if (obj.tag == "Triangle" && obj.GetComponent<MagicRingSign> ().Judge ()) {
					PlayerSkill sign = obj.GetComponent<MagicRingSign> ().sign;
                    if (magicRingWave[sign.Wave].Charge(sign.Tag))
                    {
                        magicRingShoot.Shooting();
                    }
                    
				} else {
					GunBreak ();
				}
                Destroy(obj);
            }
		}
		DrawUI ();
	}

	void CreateSign(PlayerSkill sign){
		GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/MagicRingSign/" + sign.Shape));
		obj.transform.parent = this.transform;
		obj.transform.localEulerAngles = new Vector3 (0, 0, 180);
		obj.gameObject.name = sign.Shape;
		obj.GetComponent<MagicRingSign> ().sign = sign;
		obj.GetComponent<MagicRingSign> ().SetTimer (timeOffset);
		obj.GetComponent<MagicRingSign> ().magicRingSystem = this;
	}

	void DrawUI(){
		Debug.Log ("DrawUI");
		float positionYOffset = Screen.height/2f;
		foreach (KeyValuePair<int, MagicRingWave> wave in magicRingWave) {
			Vector3 position = wave.Value.ChargeBar.GetComponent<RectTransform> ().position;
			wave.Value.ChargeBar.transform.Find ("MagicRingChargeSize/BarMask/BarPosition").GetComponent<MagicRingBar> ().SetValue (wave.Value.ChargeNum, wave.Value.ChargeCount);
			wave.Value.ChargeBar.GetComponent<RectTransform> ().position = new Vector2 (position.x, positionYOffset);
			positionYOffset = positionYOffset - 80f;
		}
	}

	public void GunBreak(){
		Debug.Log ("GunBreak");
		//Break All Sign
		//Break UI
		//Break Gun
		//Call "GameController" >> GameOver
	}
}

[System.Serializable]
public class MagicRingWave{
	public MagicRingWave(){}
	public MagicRingWave(int index, int chargeNum, GameObject chargeBar){
		Index = index;
		ChargeNum = chargeNum;
		ChargeCount = 0;
		ChargeBar = GameObject.Instantiate (chargeBar);
		ChargeBar.transform.name = index.ToString ();
		ChargeBar.GetComponent<RectTransform> ().SetParent (GameObject.Find ("ChargeUI").transform);
	}
	public int Index;
	public int ChargeNum;
	public int ChargeCount;
	public GameObject ChargeBar;
	public bool Charge (string tag){
		ChargeCount = ChargeCount + 1;
		if ((tag == "EndCharge" || tag=="OneShoot") && ChargeCount==ChargeNum) {
			Debug.Log ("Shoot");
            return true;
			//Call Shoot
			//ChargeBar Destroy
			//Remove from Dictionary
		}
        return false;
	}
}