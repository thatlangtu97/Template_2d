using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolItem : MonoBehaviour
{
    public abstract void Create();
    public abstract void Spawn();
    public abstract void Recycle();
}
