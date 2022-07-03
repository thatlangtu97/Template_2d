using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopManager : MonoBehaviour
{
    private static HitStopManager instance;
    
    public static HitStopManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject temp = new GameObject();
                temp.name = "HitStopManager";
                instance = temp.AddComponent<HitStopManager>();
            }

            return instance;
        } 
    }

    public float slowdownTime = .5f;
    public float slowMotionValue = 0.1f;
    public float checkPointTimeScale = 0.4f;
    private float tempTimeScale;
    private void Update()
    {
        tempTimeScale +=(1f / slowdownTime) * Time.unscaledDeltaTime;
        tempTimeScale = Mathf.Clamp01(tempTimeScale);
        if (tempTimeScale >= checkPointTimeScale)
        {
            //tempTimeScale = 1f;
            Time.timeScale = tempTimeScale;
        }
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ActionHitStop()
    {
        Time.timeScale = slowMotionValue;
        tempTimeScale = slowMotionValue;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ActionSlowmotion()
    {
        
    }

    public static void HitStop()
    {
        Instance.ActionHitStop();
    }
}
