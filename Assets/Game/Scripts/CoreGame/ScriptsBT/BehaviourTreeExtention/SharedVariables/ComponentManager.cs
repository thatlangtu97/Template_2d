using System;
using BehaviorDesigner.Runtime;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Doozy.Engine.Extensions;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = System.Object;

namespace Core.GamePlay
{
    public class ComponentManager : MonoBehaviour
{

    [ChildGameObjectsOnly]
    [FoldoutGroup("REFERENCE")] public StateMachineController stateMachine;
    [ChildGameObjectsOnly]
    [FoldoutGroup("REFERENCE")] public Rigidbody2D rgbody2D;
    [ChildGameObjectsOnly]
    [FoldoutGroup("REFERENCE")] public Animator animator;
    [HideInInspector]
    [FoldoutGroup("REFERENCE")] public EntityLink link;
    [FoldoutGroup("REFERENCE")] public GameEntity entity;
    [HideInInspector]
    [FoldoutGroup("REFERENCE")] public DamageInfoEvent damageInfoEvent;
    
    [ChildGameObjectsOnly]
    [FoldoutGroup("REFERENCE")] public GameObject render;
    
    [ChildGameObjectsOnly]
    [FoldoutGroup("REFERENCE")] public Collider2D collider;
    [FoldoutGroup("BUFFER")] public Vector2 vectorMove ;
    [ShowInInspector]
    [FoldoutGroup("BUFFER")] public List<Immune> currentImunes= new List<Immune>();
    [FoldoutGroup("PROPERTIES")] public float maxSpeedMove = 2f;
    [FoldoutGroup("PROPERTIES")] public bool isDoubleJump = false;
    [FoldoutGroup("PROPERTIES")] public List<Immune> baseImmunes = new List<Immune>();
    [FoldoutGroup("PROPERTIES")] public LayerMask layerMaskGround;
    [FoldoutGroup("PROPERTIES")] public Vector2 boxCheckGround;
    [ShowInInspector]
    public List<AutoAddComponent> AutoAdds = new List<AutoAddComponent>();

    public bool autoSetupEntity;
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
    
    private void OnValidate()
    {
        var components = GetComponentsInChildren<AutoAddComponent>();
        foreach (var component in components)
        {
            if(AutoAdds.Contains(component)) continue;
            AutoAdds.Add(component);
        }
    }
    private void Awake()
    {
        ComponentManagerUtils.AddComponent(this);
        ComponentManagerUtils.AddComponent(rgbody2D);
        CloneImune();
//        SetupAnim(animator);
    }

    private void Start()
    {
        if(autoSetupEntity)
            SetupEntity();
    }

    public void CloneImune()
    {
        currentImunes = baseImmunes.Clone();
    }
    [Button("SETUP ENTITY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void SetupEntity()
    {
        if (entity != null) return;
        entity = PoolManager.SpawnEntity();
        link = gameObject.Link(entity);
        foreach (var component in AutoAdds)
        {
            component.AddComponent(entity);
        }
    }
    private void OnDisable()
    {
        if (entity == null)
        {
            return;
        }
        DestroyEntity();
    }
    public void PingPong()
    {
        transform.DOScale(Vector3.one*.8f, 0.1f ).onComplete+= () =>
        {
            transform.DOScale(Vector3.one, 0.05f);
        };
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
    
    public bool IsGround
    {
        get
        {
            RaycastHit2D hit = Physics2D.BoxCast((Vector2) transform.position, boxCheckGround, 0, Vector2.down, 0f,
                layerMaskGround);
            if (hit.collider != null)
            {
                isDoubleJump = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
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
        if (vectorMove == Vector2.zero)
        {
                
        }
        else
        {
            transform.right = new Vector3(vectorMove.x,0);
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
    }

    public void AddImunes(Immune immunesAdd)
    {
        List<Immune> tempImmune = new List<Immune>();
        foreach (var immuneItem in baseImmunes)
        {
            tempImmune.Add(immuneItem);
        }
        if (!baseImmunes.Contains(immunesAdd)) ;
        {
            tempImmune.Add(immunesAdd);
        }
        currentImunes = tempImmune;

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
    public void RemoveImmunes(Immune immunesRemove)
    {
        if (baseImmunes.Contains(immunesRemove))
            return;
        if(currentImunes.Contains(immunesRemove))
            currentImunes.Remove(immunesRemove);
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

