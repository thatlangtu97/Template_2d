using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemNotActive : PoolItem
{
    public override void Create()
    {
        transform.position = Vector3.left * 10000f;
        gameObject.SetActive(true);
    }

    public override void Spawn()
    {
    }

    public override void Recycle()
    {
        transform.position = Vector3.left * 10000f;
    }
}
