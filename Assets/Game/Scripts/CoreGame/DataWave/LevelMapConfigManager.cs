using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapConfigManager : MonoBehaviour
{
    private static LevelMapConfigManager instance;

    public static LevelMapConfigManager Instance
    {
        get { return instance; }
    }
    
    public ZoneData zoneData;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
