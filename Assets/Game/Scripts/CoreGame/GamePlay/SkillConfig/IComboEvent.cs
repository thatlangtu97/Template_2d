using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using DG.Tweening;
using Object = System.Object;

namespace Core.GamePlay
{
    public enum PowerCollider
{
    Node,
    Small,
    Medium,
    Heavy,
    KnockDown,
}
public enum TypeComponent
{
    Renderer,
    Collider,
    HPBar,
    BehaviourTree,
    InteractiveGrass,
    
}
public enum TypeSpawn
{
    Transform,
    RigidBody2D,
    Forward,
    
}
public enum ColliderCast
{
    Box,
    Circle,
}
public interface IComboEvent 
{

    int id { get; set; }
    float timeTrigger { get; }
    void OnEventTrigger(GameEntity entity);
    void OnUpdateTrigger();
    void Recycle();
}

#region CAST ADD FORCE
public class CastAddForce : IComboEvent
{
    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile", true, true)]
    [HideInEditorMode()]
    public int idEvent;

    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("CAST ADD FORCE")]
    //[BoxGroup("Cast Projectile")]
    public Vector3 force;



    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        Rigidbody2D baseRigidbody = entity.stateMachineContainer.value.componentManager.rgbody2D;
        Transform baseTransform = entity.stateMachineContainer.value.transform;
        
        Vector3 CalculateForce = new Vector3(force.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
            force.y,
            force.z);
        baseRigidbody.AddForce(CalculateForce);
    }

    public void Recycle()
    {
    }

    public void OnUpdateTrigger()
    {
    }
}
#endregion

/// <summary>
/// //////////////////////
/// </summary>

#region ENABLE COMPONENT
public class EnableComponent : IComboEvent
{
    [FoldoutGroup("ENABLE COMPONENT")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("ENABLE COMPONENT")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("ENABLE COMPONENT")]
    public bool enable;

    [FoldoutGroup("ENABLE COMPONENT")] 
    public TypeComponent Component;
    
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        switch (Component)
        {
            case TypeComponent.Renderer:
                GameObject render  = entity.stateMachineContainer.value.componentManager.render ;
                if (render != null)
                {
                    render.GetComponent<SpriteRenderer>().enabled = enable;
                }
                break;
            case TypeComponent.Collider:
                Collider2D colider = entity.stateMachineContainer.value.componentManager.collider;
                if (colider)
                {
                    colider.enabled = enable;
                }
            
                break;
            case TypeComponent.HPBar:
                if (entity.hasHPBar)
                {
                    entity.hPBar.value.gameObject.SetActive(enable);
                }

                break;
            case TypeComponent.BehaviourTree:
                BehaviorDesigner.Runtime.BehaviorTree bt = entity.stateMachineContainer.value
                    .GetComponent<BehaviorDesigner.Runtime.BehaviorTree>();
                if (bt != null)
                {
                    if (!enable)
                    {
                        bt.DisableBehavior();
                    }
                    else
                    {
                        bt.EnableBehavior();
                    }
                }

                break;
            case TypeComponent.InteractiveGrass:
                GameObject interactiveGrass = entity.stateMachineContainer.value.componentManager.interactiveGrass;
                if (interactiveGrass != null)
                {
                    if (!enable)
                    {
                        interactiveGrass.SetActive(false);
                    }
                    else
                    {
                        interactiveGrass.SetActive(true);
                    }
                }

                break;
        }
    }

    public void OnUpdateTrigger()
    {
        
    }

    public void Recycle()
    {
    }
}
#endregion

#region POST EVENT
public class PostEvent : IComboEvent
{
    [FoldoutGroup("POST EVENT")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("POST EVENT")]
    [Range(0f, 5f)]
    public float timeTriggerEvent;

    [FoldoutGroup("POST EVENT")]
    public EventID eventID;
    
    
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    public void OnEventTrigger(GameEntity entity)
    {
        this.PostEvent(eventID);
        
    }

    public void OnUpdateTrigger()
    {
        
    }

    public void Recycle()
    {
    }
}
#endregion

#region SPAWN GAMEOBJECT
public class SpawnGameObject : IComboEvent
{
    [FoldoutGroup("SPAWN GAMEOBJECT")][ReadOnly] public int idEvent;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public float timeTriggerEvent;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public float duration = 0.5f;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public GameObject Prefab;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 localPosition;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 LocalRotation;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public Vector3 LocalScale = Vector3.one;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]    [ShowIf("typeSpawn", TypeSpawn.RigidBody2D)]     public Vector2 ForceSpawn ;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool setParent ;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool UseDuration;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public bool forceWhenFinishEvent ;

    [FoldoutGroup("SPAWN GAMEOBJECT")]     public TypeSpawn typeSpawn;
    
    [FoldoutGroup("SPAWN GAMEOBJECT")]     public LayerMask LayerMask ;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        if (Prefab)
        {
            Transform baseTransform = entity.stateMachineContainer.value.transform;
            switch (typeSpawn)
            {
                case TypeSpawn.Transform:
                    //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition,Quaternion.Euler(LocalRotation), LocalScale);
                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,Quaternion.Euler(LocalRotation), LocalScale);
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMask,prefabSpawned.transform,baseTransform);
                    break;
                case TypeSpawn.RigidBody2D:
                    //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition,Quaternion.Euler(LocalRotation), LocalScale);
                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition,Quaternion.Euler(LocalRotation), LocalScale);
                    
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMask,prefabSpawned.transform,baseTransform);
                    Rigidbody2D rg = prefabSpawned.GetComponent<Rigidbody2D>();
                    if (rg)
                    {
                        rg.AddForce(new Vector2(ForceSpawn.x*prefabSpawned.transform.localScale.x,ForceSpawn.y *prefabSpawned.transform.localScale.y));
                    }
                    break;
                case TypeSpawn.Forward:
                    Vector3 localScaleCalculate = new Vector3(LocalScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
                        LocalScale.y,
                        LocalScale.z);
            
                    Collider2D[] cols = null;
            
                    cols = Physics2D.OverlapCircleAll(baseTransform.position, 100, LayerMask);
                    if (cols != null)
                    {
                        foreach (var col in cols)
                        {
                            if (col != null)
                            {
                                Vector3 direction = col.transform.position - baseTransform.position;
                                Vector3 rightTransform = direction.normalized  * baseTransform.localScale.x ;
                                //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition, rightTransform, localScaleCalculate);
                                prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition, rightTransform, localScaleCalculate);
                                break;
                            }
                        }
                    }
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    //ObjectPool.instance.Recycle(prefabSpawned, duration);
                    PoolManager.Recycle(prefabSpawned, duration);
                    
                    break;
            }

            if (UseDuration)
            {
                //ObjectPool.instance.Recycle(prefabSpawned, duration);
                PoolManager.Recycle(prefabSpawned, duration);
            }
        }
    }
    public void Recycle()
    {
        if (UseDuration)
        {
            if (forceWhenFinishEvent)
            {
                if (prefabSpawned)
                {
                    //ObjectPool.Recycle(prefabSpawned);
                    PoolManager.Recycle(prefabSpawned);
                }
            }
            else
            {
                if (prefabSpawned)
                    prefabSpawned.transform.parent = null;
            }
        }
    }

    public void OnUpdateTrigger()
    {
        
    }

    void UseRayCast(Vector2 fromPosition,Vector2 direction,float distance,int layerMask,Transform transform ,Transform parent)
    {
        RaycastHit2D hit = Physics2D.Raycast(parent.position, new Vector2(1,0)* parent.localScale.x, Mathf.Abs(localPosition.x), LayerMask);
        if (hit.collider != null)
        {
            transform.position = new Vector3(hit.point.x,transform.position.y,transform.position.z); 
        }
    }
}
#endregion

#region COLLIDER EVENT
public class ColliderEvent : IComboEvent
{
    [FoldoutGroup("COLLIDER EVENT")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("COLLIDER EVENT")]
    public float timeTriggerEvent;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public LayerMask layerMaskEnemy;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [EnumToggleButtons]
    public ColliderCast typeCast;
    
    [FoldoutGroup("COLLIDER EVENT")]
    public Vector3 localPosition;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("typeCast", ColliderCast.Box)]
    public Vector3 sizeBox;
    
    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("typeCast", ColliderCast.Circle)]
    public float radius;

    [FoldoutGroup("COLLIDER EVENT")]
    public bool castByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public int idStartCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public float timeStartCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public float timeStepCastByTime;

    [FoldoutGroup("COLLIDER EVENT")]
    [ShowIf("castByTime")]
    public int maxCastByTime;

//    [FoldoutGroup("COLLIDER EVENT")]
//    public bool useColliderComponent;
//    
//    [FoldoutGroup("COLLIDER EVENT")] 
//    [ShowIf("useColliderComponent")] 
//    public float duration;
    
//    [FoldoutGroup("COLLIDER EVENT")] 
//    [ShowIf("useColliderComponent")] 
//    public bool setParen;

//    [ShowIf("useColliderComponent")] 
//    public GameObject prefab;
    
    [FoldoutGroup("COLLIDER EVENT")] 
    public DamageInfoEvent damageInfoEvent;
    
//    [FoldoutGroup("COLLIDER EVENT")]
    [BoxGroup("SOURCE")]
    [Title("Source when cast success", "", TitleAlignments.Centered)]
    [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
    [HideReferenceObjectPicker]
    public HitPhase hitPhaseData = new HitPhase();
    
    [LabelText("ON HIT PHASE")] 
    
//    [FoldoutGroup("COLLIDER EVENT")]
    [BoxGroup("TARGET")]
    [Title("Target when hit", "", TitleAlignments.Centered)]
    [GUIColor(1, 0.6f, 0.4f)]
    [HideReferenceObjectPicker]
    public HitPhase targetHitPhaseData = new HitPhase();
    
//    private GameObject col;
    private int countCast;
    private float countDuration;
//    private DamageCollider damageCollider;
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    
    public void OnEventTrigger(GameEntity entity)
    {   
        Collider2D[] cols = null;
        Transform transform = entity.stateMachineContainer.value.transform;
        Vector3 point = Vector3.zero;
        
        switch (typeCast)
        {
            case ColliderCast.Box:
                point = transform.position + new Vector3((localPosition.x + (sizeBox.x / 2f)) * (transform.right.x>=0? 1f : -1f), localPosition.y, localPosition.z);
                
                float angle  = transform.right.x > 0 ? 0f : 180f;
//                if (useColliderComponent)
//                {
//                    countDuration = 0;
//                    col = PoolManager.Spawn(prefab);
//                    damageCollider = col.GetComponent<DamageCollider>();
//                    damageCollider.SetCollider(typeCast, sizeBox, entity.power.value, damageInfoEvent, entity);
//                    col.transform.position = point;
//                    if (setParen)
//                    {
//                        col.transform.parent = transform;
//                    }
//                }
//                else
//                {
                    cols = Physics2D.OverlapBoxAll(point, sizeBox, angle, layerMaskEnemy);
                    if (cols != null)
                    {
                        
                        foreach (var col in cols)
                        {
                            if (col != null)
                            {
                                Vector2 direction = (col.transform.position - transform.position).normalized;
                                int damageProperties = entity.power.value;
                                DamageInfoEvent damageInfoEventTemp = new DamageInfoEvent(damageInfoEvent);
                                damageInfoEventTemp.forcePower = damageInfoEvent.forcePower * direction;
                                
                                HitBoxComponent hitBox =ComponentManagerUtils.GetHitBoxByInstanceId(col.gameObject.GetInstanceID());
                                ComponentManager componentManager = hitBox.component;
                                void Action()
                                {
                                    componentManager.AddForce(damageInfoEventTemp.forcePower);
                                    //componentManager.rgbody2D.AddForce(damageInfoEventTemp.forcePower,ForceMode2D.Force);
                                    
//                                    componentManager.rgbody2D.velocity = new Vector2(0,componentManager.rgbody2D.velocity.y);
//                                    componentManager.rgbody2D.AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                }
                                DamageInfoSend damageInfoSend = new DamageInfoSend(damageInfoEventTemp,damageProperties,Action);
                                DealDmgManager.DealDamage(componentManager.entity, entity,damageInfoSend);
                                if(hitPhaseData!=null)
                                    EventUpdate.SetEvent(hitPhaseData.hitPhaseEvents, entity);
                                if (targetHitPhaseData != null)
                                {
                                    EventUpdate.SetEvent(targetHitPhaseData.hitPhaseEvents,componentManager.entity);
                                }
                                
                            }
                        }
                    }
#if UNITY_EDITOR
                    GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box,angle);
#endif
//                }
                break;
            
            case ColliderCast.Circle:
                point = transform.position + new Vector3(localPosition.x * transform.localScale.x, localPosition.y, localPosition.z);
                
//                if (useColliderComponent)
//                {
//                    countDuration = 0;
//                    col = PoolManager.Spawn(prefab);
//                    
//                    damageCollider = col.GetComponent<DamageCollider>();
//                    int damageProperties = entity.power.value;
//                    damageCollider.SetCollider(typeCast, radius, damageProperties, damageInfoEvent, entity);
//                    col.transform.position = point;
//                    if (setParen)
//                    {
//                        col.transform.parent = transform;
//                    }
//                }
//                else
//                {
                    cols = Physics2D.OverlapCircleAll(point, radius, layerMaskEnemy);
                    if (cols != null)
                    {
                        foreach (var col in cols)
                        {
                            if (col != null)
                            {
                                Vector2 direction = (col.transform.position - transform.position).normalized;
                                int damageProperties = entity.power.value;
                                DamageInfoEvent damageInfoEventTemp = new DamageInfoEvent(damageInfoEvent);
                                damageInfoEventTemp.forcePower = damageInfoEvent.forcePower * direction;
                                HitBoxComponent hitBox =ComponentManagerUtils.GetHitBoxByInstanceId(col.gameObject.GetInstanceID());
                                ComponentManager componentManager = hitBox.component;
                                
                                Action action = delegate
                                {
                                    componentManager.rgbody2D.velocity = new Vector2(0,componentManager.rgbody2D.velocity.y);
                                    componentManager.rgbody2D.AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                };
                                DamageInfoSend damageInfoSend =new DamageInfoSend(damageInfoEventTemp, damageProperties, action);
                                DealDmgManager.DealDamage(componentManager.entity, entity, damageInfoSend);
                                if(hitPhaseData!=null)
                                    EventUpdate.SetEvent(hitPhaseData.hitPhaseEvents, entity);
                                if (targetHitPhaseData != null)
                                {
                                    EventUpdate.SetEvent(targetHitPhaseData.hitPhaseEvents,componentManager.entity);
                                }  
                            }
                        }
                    }
                    
//                }
#if UNITY_EDITOR
                GizmoDrawerTool.instance.draw(point, new Vector3(radius,0f,0f), GizmoDrawerTool.colliderType.Circle,0);
#endif
                break;
        }
       
    }
    public void OnUpdateTrigger()
    {
        if (castByTime)
        {
            if (countCast < maxCastByTime)
            {
                timeTriggerEvent = timeStartCastByTime + countCast * timeStepCastByTime;
                idEvent = idStartCastByTime + countCast;    
                countCast += 1;
            }
        }

//        if (useColliderComponent)
//        {
//            countDuration += Time.deltaTime;
//            if (countDuration > duration)
//            {
//                if (col)
//                {
//                    damageCollider.Recycle();
//                    //ObjectPool.Recycle(col);
//                    PoolManager.Recycle(col);
//                }
//                    
//            }
//        }
    }

    public void Recycle()
    {
        if (castByTime)
        {
            timeTriggerEvent = timeStartCastByTime;
            idEvent = idStartCastByTime;
            countCast = 0;
        }

//        if (col)
//        {
//            damageCollider.Recycle();
//            //ObjectPool.Recycle(col);
//            PoolManager.Recycle(col);
//        }
    }
}
#endregion

#region CAST PROJECTILE EVENT
public class CastProjectile : IComboEvent
{
    [FoldoutGroup("PROJECTILE")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("PROJECTILE")] 
    public float timeTriggerEvent;
    
    [FoldoutGroup("PROJECTILE")]
    public bool useDuration;
    
    [FoldoutGroup("PROJECTILE")] 
    [ShowIf("useDuration")]
    public float duration ;
    
    [FoldoutGroup("PROJECTILE")] 
    [ShowIf("useDuration")]
    public bool forceWhenFinishEvent;

    [FoldoutGroup("PROJECTILE")]  
    public bool setParent ;
    
    [FoldoutGroup("PROJECTILE")] 
    [EnumToggleButtons]
    public TypeSpawn typeSpawn;
    
    [FoldoutGroup("PROJECTILE")] 
    public GameObject Prefab;

    [FoldoutGroup("PROJECTILE")]
    public Vector3 localPosition;
    
    [FoldoutGroup("PROJECTILE")] 
    public Vector3 LocalRotation;

    [FoldoutGroup("PROJECTILE")] 
    public Vector3 LocalScale = Vector3.one;
    
    [FoldoutGroup("PROJECTILE")] 
    [ShowIf("typeSpawn", TypeSpawn.RigidBody2D)]   
    public Vector2 ForceSpawn;
    
    [FoldoutGroup("PROJECTILE")] 
    public DamageInfoEvent damageInfoEvent;

    [FoldoutGroup("PROJECTILE")]     
    public LayerMask LayerMaskLimitPosition ;
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    private GameObject prefabSpawned;
    public void OnEventTrigger(GameEntity entity)
    {
        
        if (Prefab)
        {
            Transform baseTransform = entity.stateMachineContainer.value.transform;
            switch (typeSpawn)
            {
                case TypeSpawn.Transform:
                    //prefabSpawned = ObjectPool.Spawn(Prefab,baseTransform,localPosition,Quaternion.Euler(LocalRotation),LocalScale);
                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,Quaternion.Euler(LocalRotation), LocalScale);
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(prefabSpawned.transform, baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMaskLimitPosition);
                    break;
                
                case TypeSpawn.RigidBody2D:
                    //prefabSpawned = ObjectPool.Spawn(Prefab,baseTransform,localPosition,Quaternion.Euler(LocalRotation),LocalScale);
                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,Quaternion.Euler(LocalRotation), LocalScale);
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }
                    UseRayCast(prefabSpawned.transform, baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMaskLimitPosition);
                    Rigidbody2D temp = prefabSpawned.GetComponent<Rigidbody2D>();
                    if (temp)
                    {
                        var localScale = prefabSpawned.transform.localScale;
                        temp.AddForce(new Vector2(ForceSpawn.x*localScale.x,ForceSpawn.y * localScale.y));
                    }
                    break;
                
                case TypeSpawn.Forward:
//                    Vector3 localScaleCalculate = new Vector3(LocalScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
//                                                            LocalScale.y,
//                                                            LocalScale.z);
                    Collider2D[] cols = Physics2D.OverlapCircleAll(baseTransform.position, 100, LayerMaskLimitPosition);
                    if (cols != null)
                    {
                        foreach (var col in cols)
                        {
                            if (col != null)
                            {
                                Vector3 direction = col.transform.position - baseTransform.position;
                                Vector3 rightTransform = direction.normalized  * baseTransform.localScale.x ;
                                //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition, rightTransform, LocalScale);
                                prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,rightTransform, LocalScale);
                                break;
                            }
                        }
                    }
                    if (!setParent)
                    {
                        prefabSpawned.transform.parent = null;
                    }

                    if (useDuration)
                    {
                        //ObjectPool.instance.Recycle(prefabSpawned, duration);
                        PoolManager.Recycle(prefabSpawned, duration);
                    }

                    break;
                
            }
            
            StateMachineController state = prefabSpawned.GetComponent<StateMachineController>();
            ProjectileComponent prj = prefabSpawned.GetComponent<ProjectileComponent>();
            if (state != null)
            {
                if (state.componentManager.entity != null)
                {
                    state.componentManager. damageInfoEvent = new DamageInfoEvent(damageInfoEvent);
                    state.componentManager.entity.power.value = entity.power.value;
                    
                }
            }
            if (prj != null)
            {
                if (prj.entity != null)
                {
                    prj.entity.power.value = entity.power.value; 
                    prj.projectileCollider.damageInfoEvent = new DamageInfoEvent(damageInfoEvent);
                }

            }

            if (useDuration)
            {
                //ObjectPool.instance.Recycle(prefabSpawned, duration);
                PoolManager.Recycle(prefabSpawned, duration);
            }
        }
    }
    public void Recycle()
    {
        if (useDuration)
        {
            if (forceWhenFinishEvent)
            {
                if (prefabSpawned)
                {
                    //ObjectPool.Recycle(prefabSpawned);
                    PoolManager.Recycle(prefabSpawned);
                }
            }
            else
            {
                if (prefabSpawned)
                    prefabSpawned.transform.parent = null;
            }
        }
    }

    public void OnUpdateTrigger()
    {
        
    }
    void UseRayCast(Transform transform ,Vector2 fromPosition,Vector2 direction,float distance,int layerMask)
    {
        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction,distance, LayerMaskLimitPosition);
        if (hit.collider != null)
        {
            var position = transform.position;
            position = new Vector3(hit.point.x,position.y,position.z);
            transform.position = position;
        }
    }
}
#endregion

#region SOUND
public class PlaySound : IComboEvent
{
    [FoldoutGroup("SOUND")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("SOUND")] 
    public float timeTriggerEvent;
    
    [FoldoutGroup("SOUND")] 
    public AudioClip clip;
    
    [FoldoutGroup("SOUND")] 
    [Range(0,1f)]
    public float volume = 1f;
    
    [FoldoutGroup("SOUND")] 
    public bool loop;
    
    [FoldoutGroup("SOUND")] 
    [Range(0,2f)]
    public float pitch = 1f;
    
    [FoldoutGroup("SOUND")]
    public bool useStop;
    
    [FoldoutGroup("SOUND")]
    public bool useCurveVolume;
    
    [FoldoutGroup("SOUND")]
    public bool useDuration;
    
    [FoldoutGroup("SOUND")]
    [ShowIf("useDuration")]
    public float duration;
    
    [FoldoutGroup("SOUND")] 
    [ShowIf("useCurveVolume")]
    public AnimationCurve curveVolume = new AnimationCurve(new Keyframe(0f,1f));
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }

    private float timecount;
    private AudioSource source;
    public void OnEventTrigger(GameEntity entity)
    {
        source = SoundManager.PlaySound(clip,false,volume,loop,pitch);
        timecount = 0;
    }
    public void Recycle()
    {
        if (useStop)
        {
            SoundManager.StopSound(clip);
        }
    }

    public void OnUpdateTrigger()
    {
        timecount += Time.deltaTime;
        if (source)
        {
            if(useCurveVolume)
                source.volume = volume * curveVolume.Evaluate(timecount);
            if (useDuration)
            {
                if (timecount >= duration)
                {
                    SoundManager.StopSound(clip);
                    source = null;
                }
            }
        }
    }
}
#endregion

#region SHAKE CAMERA
public class ShakeCamera : IComboEvent
{
    [FoldoutGroup("SHAKE CAMERA")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("SHAKE CAMERA")] 
    public float timeTriggerEvent;
    
    [FoldoutGroup("SHAKE CAMERA")] 
    public Vector2 strength;
    
    [FoldoutGroup("SHAKE CAMERA")]
    public float duration;
    

    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    
    public void OnEventTrigger(GameEntity entity)
    {
        CameraController.Shake(strength,duration);
    }
    public void Recycle()
    {

    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion

#region SLOW MOTION
public class SlowMotionCamera : IComboEvent
{
    [FoldoutGroup("SLOW MOTION")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("SLOW MOTION")] 
    public float timeTriggerEvent;

    [FoldoutGroup("SLOW MOTION")] 
    public float duration;
    
    [FoldoutGroup("SLOW MOTION")] 
    public float startValue;
    
    [FoldoutGroup("SLOW MOTION")] 
    public float endValue;

    [FoldoutGroup("SLOW MOTION")] 
    public float stepValue;
    

    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    
    public void OnEventTrigger(GameEntity entity)
    {
        HitStopManager.HitStop(duration, startValue , endValue, stepValue);
    }
    public void Recycle()
    {

    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion


#region INTERACTIVE GRASS
public class InteractiveGrassWind : IComboEvent
{


    [FoldoutGroup("INTERACTIVE GRASS")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("INTERACTIVE GRASS")] 
    public float timeTriggerEvent;

    [FoldoutGroup("INTERACTIVE GRASS")] 
    public float duration;
    
    
    [FoldoutGroup("INTERACTIVE GRASS")] 
    public Vector3 localPosition;
    [FoldoutGroup("INTERACTIVE GRASS")] 
    public Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode mode;
    
    [FoldoutGroup("INTERACTIVE GRASS")] 
    [ShowIf("mode", Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Translation)] 
    public GameObject FoliageObj = Resources.Load<GameObject>("FoliageTranslation");
    
    [FoldoutGroup("INTERACTIVE GRASS")] 
    [ShowIf("mode", Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Translation)] 
    public Vector2 space;
    [FoldoutGroup("INTERACTIVE GRASS")] 
    [ShowIf("mode", Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Rigidbody2D)] 
    public GameObject FoliageObjRigidbody2D = Resources.Load<GameObject>("FoliageRigidbody2D");
    
    [FoldoutGroup("INTERACTIVE GRASS")] 
    [ShowIf("mode", Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Rigidbody2D)] 
    public Vector2 velocity;
    
    [FoldoutGroup("INTERACTIVE GRASS")] 
    public Vector2 scale = new Vector2(.1f,.1f);
//    [FoldoutGroup("INTERACTIVE GRASS")] 
//    public float endValue;
//
//    [FoldoutGroup("INTERACTIVE GRASS")] 
//    public float stepValue;
    
//    public  AnimationCurve remapping = AnimationCurve.Linear(0, 0, 1, 1);
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    
    private GameObject _foliageObj;
    public void OnEventTrigger(GameEntity entity)
    {
        Transform transform = entity.stateMachineContainer.value.transform;
        switch (mode)
        {
            case Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Rigidbody2D:
                if (FoliageObjRigidbody2D)
                {
                    _foliageObj = PoolManager.Spawn(FoliageObjRigidbody2D, null, transform.position + localPosition, Quaternion.identity);
                    Vector2 _velocity = new Vector2(velocity.x * transform.right.x , velocity.y * transform.right.y);
                    _foliageObj.transform.localScale = scale;
                    _foliageObj.GetComponent<Rigidbody2D>().velocity = _velocity;
                    
                }
                break;
            case Aarthificial.PixelGraphics.Common.VelocityEmitter.EmitterMode.Translation:
                if (FoliageObj)
                {
                    _foliageObj = PoolManager.Spawn(FoliageObj, null, transform.position  + localPosition, Quaternion.identity);
                    Vector2 endPosition = new Vector2(transform.position.x + localPosition.x + transform.right.x * space.x, transform.position.y + localPosition.y + transform.right.y * space.y);
                    _foliageObj.transform.localScale = scale;
                    _foliageObj.transform.DOMove(endPosition, duration);
                }
                break;   
                
        }
        if(_foliageObj)
            PoolManager.Recycle(_foliageObj, duration);
       
    }
    public void Recycle()
    {
        
    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion

#region PLAY ANIM
public class PlayAnimation : IComboEvent
{
    [FoldoutGroup("PLAY ANIM")]
    [ReadOnly]
    public int idEvent;

    [FoldoutGroup("PLAY ANIM")] 
    public float timeTriggerEvent;

    [FoldoutGroup("PLAY ANIM")] 
    public AnimationTypeState typeAnim= AnimationTypeState.PlayAnim;
    
    [FoldoutGroup("PLAY ANIM")] 
    public string NameTrigger;
    
    [FoldoutGroup("PLAY ANIM")] 
    [ShowIf("typeAnim",AnimationTypeState.PlayAnim)]
    public float timeStart;

    [FoldoutGroup("PLAY ANIM")] 
    [ShowIf("typeAnim",AnimationTypeState.PlayAnim)]
    public int layerAnim;
    
    
    public int id { get { return idEvent; } set { idEvent = value; } }
    public float timeTrigger { get { return timeTriggerEvent; } }
    
    public void OnEventTrigger(GameEntity entity)
    {
        entity.stateMachineContainer.value.componentManager.PlayAnim(NameTrigger,typeAnim,timeStart, layerAnim);
    }
    public void Recycle()
    {

    }

    public void OnUpdateTrigger()
    {
        
    }
}
#endregion
public class HitPhase
{
    [HideReferenceObjectPicker]
    public List<IComboEvent> hitPhaseEvents = new List<IComboEvent>();
    [Button("ACCEPT MODIFY",ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]

    public void Modify()
    {
        if (hitPhaseEvents != null)
        {
            for (int i = 0; i < hitPhaseEvents.Count; i++)
            {
                
                hitPhaseEvents[i].id = i*100;
            }
        }
        else
        {
            hitPhaseEvents = new List<IComboEvent>();
        }
    }
    public HitPhase (){}
}
}
