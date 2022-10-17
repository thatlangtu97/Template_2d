using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using Sirenix.OdinInspector;
using UnityEngine;

public class DetecPlatformEffector : MonoBehaviour
{
    [ChildGameObjectsOnly]
    public BoxCollider2D Collider2D;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Check Collision When Use Platform Effector
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.position.y > transform.position.y + Collider2D.offset.y)
        {
            ComponentManager component = other.gameObject.GetComponent<ComponentManager>();
            if (component)
            {
                component.stateMachine.ChangeState(NameState.JumpState,true);
            }
        }
    }
}
