using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemParticleNotActive : PoolItem
{
    public ParticleSystem particle;
    public override void Create()
    {
        particle.Stop(true);
        transform.localScale = Vector3.zero;
    }

    public override void Spawn()
    {
        particle.Play(true);
    }

    public override void Recycle()
    {
        particle.Stop(true);
        transform.localScale = Vector3.zero;
    }

    public void OnDisable()
    {
        PoolManager.Recycle(this.gameObject);
    }
}
