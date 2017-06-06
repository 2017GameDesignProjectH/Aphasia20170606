using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleSlashScript : MonoBehaviour {

    public float LifeTime;
    public float damageValue;
    private ParticleSystem Circle;
    private float radius;

    void Start()
    {
        Circle = GetComponent<ParticleSystem>();
        radius = 0;
    }

    public void Init()
    {
        Invoke("KillYourself", LifeTime);
    }

    public void KillYourself()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnParticleCollision(GameObject other)
    {
        other.gameObject.SendMessage("Hit", damageValue);
    }

    void Update()
    {
        radius += 30*Time.deltaTime;
        ParticleSystem.ShapeModule shapeModule = Circle.shape;
        shapeModule.radius = radius;
    }
}
