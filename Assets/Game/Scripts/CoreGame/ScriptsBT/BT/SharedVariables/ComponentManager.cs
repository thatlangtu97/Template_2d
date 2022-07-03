using System;
using BehaviorDesigner.Runtime;
using Entitas.Unity;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.Extensions;
using Entitas;
using Sirenix.OdinInspector;
using UnityEngine;
[System.Serializable]
public class ComponentManager : MonoBehaviour
{
    [FoldoutGroup("REFERENCE")]
    public Transform enemy;
    [FoldoutGroup("REFERENCE")]
    public StateMachineController stateMachine;
    [FoldoutGroup("REFERENCE")]
    public Rigidbody2D rgbody2D;
    [FoldoutGroup("REFERENCE")]
    public MeshRenderer meshRenderer;
    [FoldoutGroup("REFERENCE")]
    public EntityLink link;
    [FoldoutGroup("REFERENCE")]
    [ShowInInspector]
    public GameEntity entity;
    [FoldoutGroup("REFERENCE")]
    public DamageInfoEvent damageInfoEvent;
    
    [FoldoutGroup("BUFFER")]
    public LayerMask layerMaskGround,layerMaskWall,layerEnemy, layerPlatForm;
    [FoldoutGroup("BUFFER")]
    public bool isAttack = false;
    [FoldoutGroup("BUFFER")]
    public bool isOnGround;
    [FoldoutGroup("BUFFER")]
    public bool isBufferAttack;
    [FoldoutGroup("BUFFER")]
    [Range(0f,2f)]
    public float distanceCheckGround=0.1f;
    [FoldoutGroup("BUFFER")]
    [Range(0f, 2f)]
    public float distanceCheckWall = 0.1f;
    [FoldoutGroup("BUFFER")]
    [Range(0f, 5f)]
    public float distanceChecEnemy = 0.1f;
    [FoldoutGroup("BUFFER")]
    public Vector2 vectorSpeed =Vector2.zero;
    [FoldoutGroup("BUFFER")]
    public int attackAirCount;
    [FoldoutGroup("BUFFER")]
    public float speedMove ;
    [FoldoutGroup("BUFFER")]
    public Vector2 originBoxCheckGround2d = new Vector2(.4f, .1f);
//    [FoldoutGroup("BUFFER")]
//    public float radius = .1f;
    [FoldoutGroup("BUFFER")]
    [ShowInInspector]
    public List<Immune> currentImunes= new List<Immune>();
    [FoldoutGroup("BUFFER")]
    public bool enableAI ;
    
//    [FoldoutGroup("PROPERTIES")]
//    public int heal=100;
    [FoldoutGroup("PROPERTIES")]
    public List<Immune> baseImmunes = new List<Immune>();
    [FoldoutGroup("PROPERTIES")]
    public int jumpCount,dashCount;
    [FoldoutGroup("PROPERTIES")] 
    public float maxSpeedMove = 2f;
    [FoldoutGroup("PROPERTIES")] 
    public int maxJump,maxDash, maxAttackAirCount;
    [ShowInInspector]
    //public List<IAutoAdd<GameEntity>> AutoAdds = new List<IAutoAdd<GameEntity>>();
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
//    public void OnEnable()
//    {
////        currentImunes = baseImmunes.Clone();
////        //entity = ObjectPool.instance.SpawnEntity();
////        entity = PoolManager.SpawnEntity();
////        link = gameObject.Link(entity);
//        //SetupEntity();
//    }
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
    }
    public void CloneImune()
    {
        currentImunes = baseImmunes.Clone();
    }
    [Button("SETUP ENTITY", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]

    public void SetupEntity()
    {
        //currentImunes = baseImmunes.Clone();
        //entity = ObjectPool.instance.SpawnEntity();
        entity = PoolManager.SpawnEntity();
        link = gameObject.Link(entity);
        foreach (var component in AutoAdds)
        {
            component.AddComponent(entity);
        }
//        if (entity.hasBehaviourTree)
//        {
//            entity.behaviourTree.value.DisableBehavior();
//        }
        DisableBehavior();
        //ComponentManagerUtils.AddComponent(this);
    }
    private void OnDisable()
    {
        if (entity == null)
        {
            return;
        }
//        if (entity.hasBehaviourTree)
//        {
//            entity.behaviourTree.value.DisableBehavior();
//        }
        DisableBehavior();
        DestroyEntity();
    }
    public void OnInputChangeFacing()
    {
        if(enemy)
        if (enemy.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            speedMove = -maxSpeedMove;
        }
        else if (enemy.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            speedMove = maxSpeedMove;
        }
    }
    public void ResetJumpCount()
    {
        jumpCount = 0;
    }
    
    public void ResetDashCount()
    {
        dashCount = 0;
    }
    
    public void ResetAttackAirCount()
    {
        attackAirCount = 0;
    }
    
    public bool CanJump
    {
        get { return jumpCount < maxJump; }
    }
    
    public bool CanDash
    {
        get { return dashCount < maxDash; }
    }
    
    public bool CanAttackAir
    {
        get { return attackAirCount < maxAttackAirCount; }
    }
    
    public bool checkGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position , originBoxCheckGround2d,0, Vector2.down,0f, layerMaskGround);
        if (hit.collider != null)
        {
            isOnGround = true;
            return true;
        }
        else
        {
            isOnGround = false;
            return false;
        }
    }

//    public bool CheckGroundFlatform()
//    {
//
//            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distanceCheckGround, layerPlatForm);
//            if (hit.collider != null)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//    }
    
//    public bool checkGroundBoxCast
//    {
//        get
//        {
//            int layerMask = layerMaskGround;
////            Vector2 origin = (Vector2)transform.position + originBoxCheckGround2d;
////            float radius = this.radius;
////            Vector2 direction = Vector2.zero;
////            float distance = 0;
////            RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);
//            
//            RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position , originBoxCheckGround2d,0, Vector2.down,0f, layerMask);
//            if (hit.collider != null)
//            {
//                isOnGround = true;
//                return true;
//            }
//            else
//            {
//                isOnGround = false;
//                return false;
//            }
//        }
//    }
    public bool checkWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1,0)* transform.localScale.x, distanceCheckWall, layerMaskWall);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool checkEnemyForwark()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1, 0) * transform.localScale.x, distanceChecEnemy, layerEnemy);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Rotate()
    {
        if (speedMove == maxSpeedMove)
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
            //ObjectPool.instance.RecycleEntity(entity);
            PoolManager.RecycleEntity(entity);
            entity = null;
            link = null;
        }
    }

    private void OnDestroy()
    {
        DestroyEntity();
    }

//    private void Reset()
//    {
//        Debug.Log("Reset");
//        DestroyEntity();
//        SetupEntity();
//    }

    /// <summary>
    /// UpdateWorldTransform() in Script 
    /// SkeletonAnimation.cs
    /// SkeletonMecanim.cs
    /// Skeleton.cs
    /// SkeletonGraphic.cs
    /// </summary>
//    public Spine.Unity.SkeletonMecanim mecanim;
//    [Header("Update Skeleton")]
//    public bool UpdateWorldTransform;
//    public void UpdateMecanim()
//    {
//        if (UpdateWorldTransform)/* return;*/
//        {
//            mecanim.skeleton.UpdateCache();
//            mecanim.skeleton.UpdateWorldTransform();
//        }
//    }

    public void AddImunes(List<Immune> immunesAdd)
    {
        
//        List<Immune> tempImmune = baseImmunes.Clone();
//        foreach (Immune immuneItem in immunesAdd)
//        {
//            if(baseImmunes.Contains(immuneItem))
//                continue;
//            tempImmune.Add(immuneItem);
//        }
//
//        currentImunes = tempImmune;

        
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
