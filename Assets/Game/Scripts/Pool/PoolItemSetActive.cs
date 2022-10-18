using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemSetActive : PoolItem
{
    public override void Create()
    {
        gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        gameObject.SetActive(true);
    }

    public override void Recycle()
    {
        gameObject.SetActive(false);
    }
}
