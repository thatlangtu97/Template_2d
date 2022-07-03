using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabInitInfo<T1> where T1 : struct, IComparable, IFormattable, IConvertible
{
    private readonly T1 type;
    private readonly Action<T1> onClick;
    private readonly bool isLock;

    public TabInitInfo(T1 type, Action<T1> onClick)
    {
        this.type = type;
        this.onClick = onClick;
        this.isLock = isLock;
    }


    public T1 Type
    {
        get { return type; }
    }

    public Action<T1> GetCallBack
    {
        get { return onClick; }
    }

    public bool IsLock
    {
        get { return isLock; }
    }
}