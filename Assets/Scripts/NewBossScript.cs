using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewBossScript : MonoBehaviour
{

    //public CollisionListScript AttackSensor;
    public GameObject Player;
    public GameObject DarkMist;
    public GameObject FireBall;
    //public GameObject Slash;
    public GameObject CircleSlash;
    public GameObject Laser;
    public GameObject Thunder;
    //public Slider HPBar;
    //public string motion;
    public float MinimumPeriod;
    public float FarDistance;
    public float Distance;
    public AudioSource Music;

    private Rigidbody rigidBody;
    //private bool fire;
    //private bool timeflag;


    // Use this for initialization
    void Start()
    {
        // Play darkmist
        GameObject newMist = GameObject.Instantiate(DarkMist);
        newMist.transform.position = this.transform.position;
        newMist.SetActive(true);
    }

    public void DoSlash(string NoteType)
    {
        float offset = Distance/75;
        GameObject newSlash = (GameObject)Instantiate(Resources.Load("Prefabs/Skills/" + NoteType));
        newSlash.SetActive(true);
        SlashScript slash = newSlash.GetComponent<SlashScript>();
        slash.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y - 0.2f, this.transform.position.z);
        slash.transform.rotation = this.transform.rotation;
        slash.InitAndShoot(new Vector3(
            (Player.transform.position.x - this.transform.position.x) + Player.GetComponent<Rigidbody>().velocity.x * offset, 0,
            (Player.transform.position.z - this.transform.position.z) + Player.GetComponent<Rigidbody>().velocity.z * offset
            ).normalized);
    }

    public void DoTripleSlash()
    {
        for (int i = -1; i < 2; i++)
        {
            int t = Random.Range(0, 3);
            float offset = Distance/75;
            offset *= i;
            string type = t.ToString();
            GameObject newSlash = (GameObject)Instantiate(Resources.Load("Prefabs/Skills/" + "Note_0" + type));
            newSlash.SetActive(true);
            SlashScript slash = newSlash.GetComponent<SlashScript>();
            slash.transform.position = new Vector3(this.transform.position.x,
                this.transform.position.y - 0.2f, this.transform.position.z);
            slash.transform.rotation = this.transform.rotation;
            slash.InitAndShoot(new Vector3(
                (Player.transform.position.x - this.transform.position.x) + Player.GetComponent<Rigidbody>().velocity.x * offset, 0,
                (Player.transform.position.z - this.transform.position.z) + Player.GetComponent<Rigidbody>().velocity.z * offset
                ).normalized);
        }
    }

    public void DoCircleSlash()
    {
        GameObject newCircleSlash = GameObject.Instantiate(CircleSlash);
        newCircleSlash.SetActive(true);
        CircleSlashScript slash = newCircleSlash.GetComponent<CircleSlashScript>();
        slash.transform.position = new Vector3(this.transform.position.x,
            this.transform.position.y, this.transform.position.z);
        slash.Init();
    }

    public void DoThunder()
    {
        GameObject newThunder = GameObject.Instantiate(Thunder);
        newThunder.SetActive(true);
        newThunder.transform.position = new Vector3(Player.transform.position.x,
            this.transform.position.y, Player.transform.position.z);
    }

    public void DoLaser(int mode)
    {
        GameObject newLaser = GameObject.Instantiate(Laser);
        newLaser.SetActive(true);
        LaserScript laser = newLaser.GetComponent<LaserScript>();
        laser.Music = Music;
        Quaternion spin = Quaternion.identity;
        spin.eulerAngles = this.transform.rotation.eulerAngles + new Vector3(0, -60, 0);
        laser.transform.position = new Vector3(this.transform.position.x + 0.1f,
                this.transform.position.y + 0.1f, this.transform.position.z + 0.1f);
        laser.transform.rotation = spin;
        laser.InitAndShoot(mode);
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust lookat
        Vector3 lookAt = Player.transform.position;
        lookAt.y = this.gameObject.transform.position.y;
        this.transform.LookAt(lookAt);
        Distance = (this.transform.position - Player.transform.position).magnitude;
    }
}
