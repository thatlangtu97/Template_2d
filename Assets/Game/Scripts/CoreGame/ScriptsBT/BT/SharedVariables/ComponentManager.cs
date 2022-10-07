using System;
using BehaviorDesigner.Runtime;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Extensions;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = System.Object;

namespace Core.GamePlay
{
    public class ComponentManager : MonoBehaviour
{
    [HideInInspector]
    [FoldoutGroup("REFERENCE")] public Transform enemy;
    [FoldoutGroup("REFERENCE")] public StateMachineController stateMachine;
    [FoldoutGroup("REFERENCE")] public Rigidbody2D rgbody2D;
    [FoldoutGroup("REFERENCE")] public Animator animator;
    [HideInInspector]
    [FoldoutGroup("REFERENCE")] public EntityLink link;
    [FoldoutGroup("REFERENCE")] public GameEntity entity;
    [HideInInspector]
    [FoldoutGroup("REFERENCE")] public DamageInfoEvent damageInfoEvent;
    [FoldoutGroup("REFERENCE")] public Object render;
    [FoldoutGroup("BUFFER")] public float speedMove ;
    [ShowInInspector]
    [FoldoutGroup("BUFFER")] public List<Immune> currentImunes= new List<Immune>();
    [FoldoutGroup("BUFFER")] public bool enableAI;
    [FoldoutGroup("PROPERTIES")] public float maxSpeedMove = 2f;
    [FoldoutGroup("PROPERTIES")] public List<Immune> baseImmunes = new List<Immune>();
    [ShowInInspector]
    public List<AutoAddComponent> AutoAdds = new List<AutoAddComponent>();
    
    [Button("FIND AUTO ADD COMPONENT", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    void FindComponentEntitas()
    { 
        var components = GetComponentsInChildren<AutoAddComponent>();
        foreach (var component in components)
        {
            if(AutoAdds.Contains(component)) continue;
            AutoAdds.Add(component);
        }
    }
    
    public void DisableBehavior()
    {
        enableAI = false;
    }

    public void EnableBehavior()
    {
        enableAI = true;
    }
    private void Awake()
    {
        ComponentManagerUtils.AddComponent(this);
        ComponentManagerUtils.AddComponent(rgbody2D);
        CloneImune();
//        SetupAnim(animator);
    }
    public void CloneImune()
    {
        currentImunes = baseImmunes.Clone();
    }
    [Button("SETUP ENTITY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupEntity()
    {
        entity = PoolManager.SpawnEntity();
        link = gameObject.Link(entity);
        foreach (var component in AutoAdds)
        {
            component.AddComponent(entity);
        }
        DisableBehavior();
    }
    private void OnDisable()
    {
        if (entity == null)
        {
            return;
        }
        DisableBehavior();
        DestroyEntity();
    }
    public void OnInputChangeFacing()
    {
//        if(enemy)
//            
//        if (enemy.transform.position.x < transform.position.x)
//        {
//            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//            speedMove = -maxSpeedMove;
//        }
//        else if (enemy.transform.position.x > transform.position.x)
//        {
//            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//            speedMove = maxSpeedMove;
//        }
    }
//    public void ResetJumpCount()
//    {
//        jumpCount = 0;
//    }
//    
//    public void ResetDashCount()
//    {
//        dashCount = 0;
//    }
    
//    public void ResetAttackAirCount()
//    {
//        attackAirCount = 0;
//    }
    
//    public bool CanJump
//    {
//        get { return jumpCount < maxJump; }
//    }
//    
//    public bool CanDash
//    {
//        get { return dashCount < maxDash; }
//    }
//    
//    public bool CanAttackAir
//    {
//        get { return attackAirCount < maxAttackAirCount; }
//    }
    
//    public bool checkGround()
//    {
//        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position , originBoxCheckGround2d,0, Vector2.down,0f, layerMaskGround);
//        if (hit.collider != null)
//        {
//            isOnGround = true;
//            return true;
//        }
//        else
//        {
//            isOnGround = false;
//            return false;
//        }
//    }
//    public bool checkWall()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1,0)* transform.localScale.x, distanceCheckWall, layerMaskWall);
//        if (hit.collider != null)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
//    public bool checkEnemyForwark()
//    {
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0) * transform.localScale.x, distanceChecEnemy, layerEnemy);
//        if (hit.collider != null)
//        {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }
    public void PlayAnim(string name, AnimationTypeState type , float timestart)
    {
        if (animator)
        {
            switch (type)
            {
                case AnimationTypeState.Trigger:
                    animator.SetTrigger(name);
                    break;
                case AnimationTypeState.PlayAnim:
                    animator.Play(name,0,timestart);
                    break;
            }
        }
    }
    public void SetSpeed(float speed)
    {
        if (animator)
        {
            animator.speed = speed;
        }
    }
    public void Rotate()
    {
        if (speedMove > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (speedMove == -maxSpeedMove)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    public void DestroyEntity()
    {
        if (entity != null)
        {
            gameObject.Unlink();
            PoolManager.RecycleEntity(entity);
            entity = null;
            link = null;
        }
    }
    private void OnDestroy()
    {
        DestroyEntity();
    }
    public void AddImunes(List<Immune> immunesAdd)
    {
        List<Immune> tempImmune = new List<Immune>();
        foreach (var immuneItem in baseImmunes)
        {
            tempImmune.Add(immuneItem);
        }
        foreach (var immuneItem in immunesAdd)
        {
            if (baseImmunes.Contains(immuneItem)) ;
            {
                continue;
            }
            tempImmune.Add(immuneItem);
        }
        currentImunes = tempImmune;
        //new addimune 
    }
    public void RemoveImmunes(List<Immune> immunesRemove)
    {
        foreach (Immune immuneItem in immunesRemove)
        {
            if(baseImmunes.Contains(immuneItem))
                continue;
            if(currentImunes.Contains(immuneItem))
                currentImunes.Remove(immuneItem);
        }
    }
    public bool HasImmune(Immune immune)
    {
        if (currentImunes.Contains(immune))
        {
            return true;
        }
        return false;
    }
}
}

