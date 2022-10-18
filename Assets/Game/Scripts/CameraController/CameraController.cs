using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public ProCamera2DShake proCamera2DShake;

    [FoldoutGroup("CAMERA LIMIT")]
    public float minX, maxX;          
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
    }
    public void ShakeCamera(Vector2 strength, float duration )
    {
        proCamera2DShake.Strength = strength;
        proCamera2DShake.Duration = duration;
        proCamera2DShake.Shake();
    }

    public static void Shake(Vector2 strength, float duration )
    {
        if (instance != null)
        {
            instance.ShakeCamera(strength, duration);
        }
    }
}
