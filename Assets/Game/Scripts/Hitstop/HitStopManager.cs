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

    public float slowdownTime = 0f;
    public float slowMotionValue = 0.1f;
    public float checkPointTimeScale = 0.4f;
    public float stepValue=0.02f;
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
        Time.fixedDeltaTime = Time.timeScale * stepValue;
    }

    public void ActionHitStop()
    {
        Time.timeScale = slowMotionValue;
        tempTimeScale = slowMotionValue;
        Time.fixedDeltaTime = Time.timeScale * stepValue;
    }

    public void ActionHitStop(float slowdownTime , float slowMotionValue, float checkPointTimeScale, float stepValue)
    {
        this.slowdownTime = slowdownTime;
        this.slowMotionValue = slowMotionValue;
        this.checkPointTimeScale = checkPointTimeScale;
        ActionHitStop();

    }

    public void SlowMotion(float slowdownTime , float slowMotionValue, float checkPointTimeScale, float stepValue)
    {
        ActionHitStop(slowdownTime, slowMotionValue, checkPointTimeScale, stepValue);
    }
    public void ActionSlowmotion()
    {
        
    }

//    public static void HitStop()
//    {
//        Instance.ActionHitStop();
//    }

    public static void HitStop(float slowdownTime , float slowMotionValue , float checkPointTimeScale, float stepValue)
    {
        Instance.ActionHitStop(slowdownTime, slowMotionValue, checkPointTimeScale , stepValue);
    }

    public static void DefaultSlowMotion()
    {
        Instance.SlowMotion(.5f, .05f, .2f, 0.01f);
    }
}
