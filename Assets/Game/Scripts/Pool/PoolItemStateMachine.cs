using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Core.GamePlay;
using UnityEngine;

public class PoolItemStateMachine : PoolItem
{
    public StateMachineController stateMachine;
    public override void Create()
    {
        transform.position = Vector3.left * 10000f;
        gameObject.SetActive(true);
        GetComponent<Rigidbody2D>().isKinematic = true;
        
    }

    public override void Spawn()
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public override void Recycle()
    {
        transform.position = Vector3.left * 10000f;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
