using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

public class HitBoxComponent : MonoBehaviour
{
    public ComponentManager component;

    public void Awake()
    {
        ComponentManagerUtils.AddComponent(this);
    }

    private void OnValidate()
    {
        if(!component)
            component = GetComponentInParent<ComponentManager>();
    }
}
