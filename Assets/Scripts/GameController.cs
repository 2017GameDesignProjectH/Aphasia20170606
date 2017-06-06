using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public BattleDataLoding DataLoading;
    public StageInfo stageInfo;
    public NewBossScript BossController;
    public MagicRingSystem magicRingSystem;
    public GameObject PlayerController;
    public GameObject Player;
    public AudioSource Music;
    public float dashspeed;
    public float MinAttackPeriod;
    public float MinDashPeriod;
    public float time;

    private int skillNum;
    // private bool flag;
    private float AttackCounter;
    private float DashCounter;

	public GameObject mainCamera1;
	public GameObject mainCamera2;
	public GameObject magicRing;
	public int cameraNum = 0;
	public PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        skillNum = 0;
        // flag = false;
        DataLoading.LoadingStage("PIANO.json");
        stageInfo = DataLoading.stageInfo;
        Music.clip = (AudioClip)Resources.Load("Sounds/" + stageInfo.Music);
        Music.Play();
        // Music.time = 50; 
        magicRingSystem.SettingPlayerSkills(stageInfo);
        magicRingSystem.SettingStageMusic(Music);
    }

    // Update is called once per frame
    void Update()
    {
        time = Music.time;

        // Player
        if (AttackCounter > 0)
        {
            AttackCounter -= Time.deltaTime;
        }

        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;
        }

		if (Input.GetKeyDown (KeyCode.JoystickButton5)) {
			if (cameraNum == 0) {
				mainCamera1.GetComponent<Camera> ().enabled = false;
				mainCamera2.GetComponent<Camera> ().enabled = true;
				magicRing.SetActive (true);
				playerController.canJump = false;
				cameraNum = 1;
			} else if (cameraNum == 1) {
				mainCamera1.GetComponent<Camera> ().enabled = true;
				mainCamera2.GetComponent<Camera> ().enabled = false;
				magicRing.SetActive (false);
				playerController.canJump = true;
				cameraNum = 0;
			}
		}

        Vector3 forward = Player.transform.forward.normalized;
        forward.y = 0;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (AttackCounter <= 0)
            {
                AttackCounter = MinAttackPeriod;
                Player.SendMessage("Attack");
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (DashCounter <= 0)
            {
                DashCounter = MinDashPeriod;
                PlayerController.transform.DOMove(new Vector3(PlayerController.transform.position.x + forward.x * dashspeed,
                    PlayerController.transform.position.y, PlayerController.transform.position.z + forward.z * dashspeed), 0.1f);
            }
        } 


        // Boss
        if (skillNum < stageInfo.BossSkills.Length && stageInfo.BossSkills[skillNum].Time <= Music.time)
        {
            if(stageInfo.BossSkills[skillNum].SkillName == "Note_00")
            {
                BossController.DoSlash("Note_00");
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Note_01")
            {
                BossController.DoSlash("Note_01");
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Note_02")
            {
                BossController.DoSlash("Note_02");
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Note_03")
            {
                BossController.DoSlash("Note_03");
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "TripleSlash_00")
            {
                BossController.DoTripleSlash();
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "CircleSlash_00")
            {
                BossController.DoCircleSlash();
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Laser_00")
            {
                BossController.DoLaser(1);
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Laser_01")
            {
                BossController.DoLaser(2);
            }

            if (stageInfo.BossSkills[skillNum].SkillName == "Thunder_00")
            {
                BossController.DoThunder();
            }

            skillNum = skillNum + 1;
        }

    }

}
