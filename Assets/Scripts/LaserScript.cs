using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserScript : MonoBehaviour
{
    public float LifeTime;
    public AudioSource Music;
    private float change;
    private float timetemp;

    public void InitAndShoot(int m)
    {
        change = 3f;
        timetemp = Music.time;
        if (m == 1)
        {
            Invoke("KillYourself", LifeTime);
        } else if (m == 2)
        {
            Invoke("KillYourself", LifeTime*2);
            Invoke("Rewind", LifeTime);
        }
    }

    public void KillYourself()
    {
        this.gameObject.SetActive(false);
    }

    public void Rewind()
    {
        change *= -1;
    }

    void Update()
    {
        if (Music.time - timetemp >= 0.01)
        {
            Quaternion spin = this.transform.rotation;
            spin.eulerAngles += new Vector3(0, change, 0);
            this.transform.rotation = spin;
            timetemp = Music.time;
        }

    }
}
