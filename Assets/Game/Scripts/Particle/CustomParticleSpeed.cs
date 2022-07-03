using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleSpeed : MonoBehaviour
{
    public AnimationCurve curceSpeed;
    public ParticleSystem particle;
    private float timeSpeed;
    private float speed;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        speed += Time.deltaTime;
        //particle.simula
    }

    public void Play()
    {
        timeSpeed = 0;
        speed = curceSpeed.Evaluate(timeSpeed);
    }

}
