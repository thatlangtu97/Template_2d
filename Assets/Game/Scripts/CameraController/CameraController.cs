using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public ProCamera2D proCamera2D;
    public ProCamera2DShake proCamera2DShake;
    public ProCamera2DNumericBoundaries proCamera2DNumericBoundaries;
    public Transform mainTarget;
    public Vector2 mainOffset;

    [FoldoutGroup("CAMERA LIMIT")]
    public float minX, maxX;          
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
    }

//    public void LateUpdate()
//    {
//        if (proCamera2D.transform.position.x < minX)
//        {
//            proCamera2D.transform.position = new Vector3(minX ,proCamera2D.transform.position.y, proCamera2D.transform.position.z);
//        }
//        else if (proCamera2D.transform.position.x > maxX)
//        {
//            proCamera2D.transform.position = new Vector3(maxX ,proCamera2D.transform.position.y,proCamera2D.transform.position.z); 
//        }
//    }
    [Button("ACCEPT MODIFY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void SetLimitTest()
    {
        SetLimitCamera(minX, maxX);
    }
    public void SetLimitCamera(float minX,float maxX)
    {
        proCamera2DNumericBoundaries.enabled = true;
        proCamera2DNumericBoundaries.LeftBoundary = minX;
        proCamera2DNumericBoundaries.RightBoundary = maxX;
    }
    public void AddTarget(Transform transform)
    {
        
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetTransform == transform)
            {
                return;
            }
        }
        proCamera2D.AddCameraTarget(transform, 1f, 1f);
    }
    public void RemoveTarget(Transform transform)
    {
        
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetTransform == transform)
            {
                proCamera2D.CameraTargets.Remove(VARIABLE);
                return;
            }
        }
    }
    public void SetMainTarget(Transform transform,Vector2 offset)
    {
        mainTarget = transform;
        mainOffset = offset;
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetTransform == transform)
            {
                VARIABLE.TargetInfluenceH = 1f;
                VARIABLE.TargetInfluenceV = 1f;
                VARIABLE.TargetOffset = mainOffset;
            }
            else
            {
                VARIABLE.TargetInfluenceH = 0f;
                VARIABLE.TargetInfluenceV = 0f;
            }
        }
    }

    public void FollowTarget(Transform transform , Vector2 offset)
    {
        
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetTransform == transform)
            {
                VARIABLE.TargetInfluenceH = 1f;
                VARIABLE.TargetInfluenceV = 1f;
                VARIABLE.TargetOffset = offset;
            }
            else
            {
                VARIABLE.TargetInfluenceH = 0f;
                VARIABLE.TargetInfluenceV = 0f;
            }
        }
    }
    
    public Transform GetMainTarget()
    {
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetInfluenceH ==1 && VARIABLE.TargetInfluenceV == 1f)
            {
                return VARIABLE.TargetTransform;
            }
        }
        return null;
    }

    public void RestoneMainTarget()
    {
        foreach (var VARIABLE in proCamera2D.CameraTargets)
        {
            if (VARIABLE.TargetTransform == mainTarget)
            {
                VARIABLE.TargetInfluenceH = 1f;
                VARIABLE.TargetInfluenceV = 1f;
                VARIABLE.TargetOffset = mainOffset;
            }
            else
            {
                VARIABLE.TargetInfluenceH = 0f;
                VARIABLE.TargetInfluenceV = 0f;
            }
        }
    }
    public void ShakeCamera(Vector2 strength, float duration )
    {
        proCamera2DShake.Strength = strength;
        proCamera2DShake.Duration = duration;
        proCamera2DShake.Shake();
    }
    
}
