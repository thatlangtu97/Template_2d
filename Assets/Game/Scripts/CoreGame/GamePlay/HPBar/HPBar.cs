using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [ChildGameObjectsOnly]
    public GameObject hpValue;

    public GameEntity entity;

    public void SetValue(float value)
    {
        hpValue.transform.localScale = new Vector3(value,hpValue.transform.localScale.y);
    }
}
