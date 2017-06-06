using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class BattleDataLoding : MonoBehaviour {

	public StageInfo stageInfo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void LoadingStage(string stageName){
		GetStageInfo (stageName);
	}

	public void GetStageInfo(string stageName){
		string DPath = Application.dataPath;
		int num = DPath.LastIndexOf ("/");
		DPath = DPath.Substring (0, num);
		string stagePath = DPath + "/StageInfo/" + stageName;
		if (File.Exists (stagePath)) {
			StageDataSplit (stagePath);
		} else {
			Debug.Log ("No such file!");
		}
	}

	public void StageDataSplit(string stagePath){
		string jsonString;
		JsonData jsonData;
		jsonString = File.ReadAllText (stagePath);
		jsonData = JsonMapper.ToObject (jsonString);
		stageInfo.StageName = (string)jsonData ["name"];
		stageInfo.Music = (string)jsonData ["music"];
		Debug.Log (jsonData ["BossSkills"].Count);
		stageInfo.BossSkills = new BossSkill[jsonData ["BossSkills"].Count];
		for (int i = 0; i < jsonData ["BossSkills"].Count; i++) {
			stageInfo.BossSkills [i] = new BossSkill ((float)(double)jsonData ["BossSkills"] [i] ["time"], (string)jsonData ["BossSkills"] [i] ["skill"]);
		}
		stageInfo.PlayerSkills = new PlayerSkill[jsonData ["PlayerSkills"].Count];
		for (int i = 0; i < jsonData ["PlayerSkills"].Count; i++) {
			stageInfo.PlayerSkills [i] = new PlayerSkill ((float)(double)jsonData ["PlayerSkills"] [i] ["time"], (string)jsonData ["PlayerSkills"] [i] ["shape"]
				, (string)jsonData ["PlayerSkills"] [i] ["tag"], (int)jsonData ["PlayerSkills"] [i] ["wave"], (int)jsonData ["PlayerSkills"] [i] ["ChargeNum"]);
		}
	}

}

[System.Serializable]
public class StageInfo{
	public StageInfo (){}
	public StageInfo(string stageName, string music){
		StageName = stageName;
		Music = music;
	}
	public string StageName;
	public string Music;
	public BossSkill[] BossSkills;
	public PlayerSkill[] PlayerSkills;
}

[System.Serializable]
public class BossSkill{
	public BossSkill(){}
	public BossSkill(float time, string skillName){
		Time = time;
		SkillName = skillName;
	}
	public float Time;
	public string SkillName;
}

[System.Serializable]
public class PlayerSkill{
	public PlayerSkill(){}
	public PlayerSkill(float time, string shape, string tag, int wave, int chargeNum){
		Time = time;
		Shape = shape;
		Tag = tag;
		Wave = wave;
		ChargeNum = chargeNum;
	}
	public float Time;
	public string Shape;
	public string Tag;
	public int Wave;
	public int ChargeNum;
}