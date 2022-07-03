using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using Unity.Mathematics;
using UnityEngine;

public class SpawnSkeletonMecanim : MonoBehaviour
{
    public SkeletonMecanim mecanimPrefab;
    public StateMachineController controller;
    public ComponentManager componentManager;
    public Vector3 localScale;
    private SkeletonMecanim tempMecanim;
    private void OnEnable()
    {
        tempMecanim = PoolManager.Spawn<SkeletonMecanim>(mecanimPrefab.gameObject,controller.transform,Vector3.zero,quaternion.identity, localScale);
        controller.animator = tempMecanim.GetComponent<Animator>();
        componentManager.meshRenderer = tempMecanim.GetComponent<MeshRenderer>();
        controller.SetupAnim(controller.animator);
    }

    private void OnDisable()
    {
        PoolManager.Recycle(tempMecanim.gameObject);
    }
}
