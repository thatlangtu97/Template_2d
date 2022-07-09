using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Drawers;
using Spine;

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
//public enum TypeComponent
//{
//    MeshRenderer,
//    
//}
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

    public enum TypeCast
    {
        Forward,
        Transform,
    }
    
    public interface IComboEvent 
    {

        int id { get; set; }
        float timeTrigger { get; }
        void OnEventTrigger(GameEntity entity);
        void OnUpdateTrigger();
        void Recycle();
    }
#region EVENT SOUND
    public class EVENT_SOUND : IComboEvent
    {
        [FoldoutGroup("EVENT SOUND")]
        [ReadOnly]
        public int idEvent;

        [FoldoutGroup("EVENT SOUND")] 
        public float timeTriggerEvent;
    
        [FoldoutGroup("EVENT SOUND")] 
        public AudioClip clip;
    
        [FoldoutGroup("EVENT SOUND")] 
        [Range(0,1f)]
        public float volume;
    
        [FoldoutGroup("EVENT SOUND")] 
        public bool loop;
    
        [FoldoutGroup("EVENT SOUND")] 
        [Range(0,2f)]
        public float pitch;
    
        [FoldoutGroup("EVENT SOUND")]
        public bool useStop;
    
        [FoldoutGroup("EVENT SOUND")]
        public bool useCurveVolume;
    
        [FoldoutGroup("EVENT SOUND")]
        public bool useDuration;
    
        [FoldoutGroup("EVENT SOUND")]
        [ShowIf("useDuration")]
        public float duration;
    
        [FoldoutGroup("EVENT SOUND")] 
        [ShowIf("useCurveVolume")]
        public AnimationCurve curveVolume = new AnimationCurve(new Keyframe(0f,1f));
    
        public int id { get { return idEvent; } set { idEvent = value; } }
        public float timeTrigger { get { return timeTriggerEvent; } }

        private float timecount;
        private AudioSource source;
        public void OnEventTrigger(GameEntity entity)
        {
            source = SoundManager.instance.playSound(clip,false,volume,loop,pitch);
            timecount = 0;
        }
        public void Recycle()
        {
            if (useStop)
            {
                SoundManager.instance.StopSound(clip);
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
                        SoundManager.instance.StopSound(clip);
                        source = null;
                    }
                }
            }
        }
    }
#endregion

#region EVENT SHAKE CAMERA
    public class EVENT_SHAKE_CAMERA : IComboEvent
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
            CameraController.instance.ShakeCamera(strength,duration);
        }
        public void Recycle()
        {

        }

        public void OnUpdateTrigger()
        {
            
        }
    }
#endregion

#region EVENT ANIMATION
    public class EVENT_ANIMATION : IComboEvent
    {
        [FoldoutGroup("EVENT ANIMATION")]
        [ReadOnly]
        public int idEvent;

        [FoldoutGroup("EVENT ANIMATION")] 
        public float timeTriggerEvent;
        
        [FoldoutGroup("EVENT ANIMATION")]
        [EnumToggleButtons, HideLabel]
        public AnimationTypeState typeAnim;
        
        [FoldoutGroup("EVENT ANIMATION")] 
        [LabelText("$typeAnim")]
        public string  nameTrigger;

        [FoldoutGroup("EVENT ANIMATION")] 
        public float timeStart;
        
        [FoldoutGroup("EVENT ANIMATION")] 
        public float duration;
        
        [FoldoutGroup("EVENT ANIMATION")]
        [GUIColor(0f, 1f, 0f)]
        [HideReferenceObjectPicker]
        public AnimationCurve curveSpeedAnimation= new AnimationCurve(new Keyframe(0,1f));

        private float timeCount;
        private StateMachineController controller;
        
        public int id { get { return idEvent; } set { idEvent = value; } }
        public float timeTrigger { get { return timeTriggerEvent; } }
        
        public void OnEventTrigger(GameEntity entity)
        {
            controller = entity.stateMachineContainer.value;
            if(controller)
                controller.PlayAnim(nameTrigger,typeAnim,timeStart);
            timeCount = 0;
        }
        public void Recycle()
        {
            timeCount = duration;
        }

        public void OnUpdateTrigger()
        {
            timeCount += Time.deltaTime;
            if (timeCount <= duration)
            {
                if(controller)
                    controller.SetSpeedAnim(curveSpeedAnimation.Evaluate(timeCount));
            }
        }
    }
#endregion
    
#region EVENT COLLIDER
    public class EVENT_COLLIDER : IComboEvent
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

    //    [FoldoutGroup("COLLIDER EVENT")]
    //    [ShowIf("useAngle")]
    //    public float angleCollider;

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

        [FoldoutGroup("COLLIDER EVENT")] 
        [HideReferenceObjectPicker]
        public DamageInfoEvent damageInfoEvent = new DamageInfoEvent();
        
        [FoldoutGroup("COLLIDER EVENT")] 
        [HideReferenceObjectPicker]
        public List<OnHitPhase> OnHitPhases = new List<OnHitPhase>();
        private int countCast;
        private DamageCollider damageCollider;
        
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
                    point = transform.position + new Vector3((localPosition.x + (sizeBox.x / 2f)) * transform.localScale.x,
                                        localPosition.y, localPosition.z);
                    float angle = 0;
    //                if (!useAngle)
    //                {
                        angle  = transform.right.x > 0 ? 0f : 180f;
    //                }
    //                else
    //                {
    //                    if (transform.localScale.x > 0)
    //                    {
    //                        angle = angleCollider;
    //                    }
    //                    else
    //                    {
    //                        angle = 180f - angleCollider;
    //                    }
    //                }

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
                                    //ComponentManager componentManager = col.GetComponent<ComponentManager>();
                                    ComponentManager componentManager =
                                        ComponentManagerUtils.GetComponentByInstanceId(col.gameObject.GetInstanceID());
                                    void Action()
                                    {
                                        componentManager.rgbody2D.velocity = new Vector2(0,componentManager.rgbody2D.velocity.y);
                                        componentManager.rgbody2D.AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                        //col.GetComponent<Rigidbody2D>().AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                    }
                                    DamageInfoSend damageInfoSend = new DamageInfoSend(damageInfoEventTemp,damageProperties,Action);
                                    //DealDmgManager.DealDamage(col, entity,damageInfoSend);
                                    DealDmgManager.DealDamage(componentManager.entity, entity,damageInfoSend);
                                    
                                }
                            }
                            if(cols.Length>0)
                                HitStopManager.HitStop();
    #if UNITY_EDITOR
                        GizmoDrawerTool.instance.draw(point, sizeBox, GizmoDrawerTool.colliderType.Box,angle);
    #endif
                    }
                    break;
                
                case ColliderCast.Circle:
                    point = transform.position + new Vector3(localPosition.x * transform.localScale.x, localPosition.y, localPosition.z);
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
                                    //ComponentManager componentManager = col.GetComponent<ComponentManager>();
                                    ComponentManager componentManager =
                                        ComponentManagerUtils.GetComponentByInstanceId(col.gameObject.GetInstanceID());
                                    Action action = delegate
                                    {
                                        componentManager.rgbody2D.velocity = new Vector2(0,componentManager.rgbody2D.velocity.y);
                                        componentManager.rgbody2D.AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                        //col.GetComponent<Rigidbody2D>().AddForceAtPosition(damageInfoEventTemp.forcePower, col.transform.position);
                                    };
                                    DamageInfoSend damageInfoSend =new DamageInfoSend(damageInfoEventTemp, damageProperties, action);
                                    //DealDmgManager.DealDamage(col, entity, damageInfoSend);
                                    DealDmgManager.DealDamage(componentManager.entity, entity, damageInfoSend);
                                }
                            }
                            if(cols.Length>0)
                                HitStopManager.HitStop();
                        }
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
        }

        public void Recycle()
        {
            if (castByTime)
            {
                timeTriggerEvent = timeStartCastByTime;
                idEvent = idStartCastByTime;
                countCast = 0;
            }
        }
    }
#endregion

#region EVENT CAST VFX
    public class EVENT_CAST_VFX : IComboEvent
    {
        [FoldoutGroup("EVENT CAST VFX")][ReadOnly] public int idEvent;

        [FoldoutGroup("EVENT CAST VFX")]  public float timeTriggerEvent;
        
        [FoldoutGroup("EVENT CAST VFX")]  public float duration = 0.5f;

        [FoldoutGroup("EVENT CAST VFX")][PreviewField]    public GameObject prefab;

        [FoldoutGroup("EVENT CAST VFX")]  public Vector3 localPosition;
        
        [FoldoutGroup("EVENT CAST VFX")]  public Vector3 localRotation;

        [FoldoutGroup("EVENT CAST VFX")]   public Vector3 localScale = Vector3.one;
        
        [FoldoutGroup("EVENT CAST VFX")] [ShowIf("typeSpawn", TypeSpawn.RigidBody2D)]     public Vector2 forceSpawn ;
        
        [FoldoutGroup("EVENT CAST VFX")]   public bool setParent ;
        
        [FoldoutGroup("EVENT CAST VFX")]   public bool useDuration;
        
        [FoldoutGroup("EVENT CAST VFX")]    public bool forceWhenFinishEvent ;

        [FoldoutGroup("EVENT CAST VFX")][EnumToggleButtons]    public TypeSpawn typeSpawn;
        
        [FoldoutGroup("EVENT CAST VFX")]   public LayerMask layerCastLimit ;
        public int id { get { return idEvent; } set { idEvent = value; } }
        public float timeTrigger { get { return timeTriggerEvent; } }
        private GameObject prefabSpawned;
        public void OnEventTrigger(GameEntity entity)
        {
            if (prefab)
            {
                Transform ownerTransform = entity.stateMachineContainer.value.transform;
                switch (typeSpawn)
                {
                    case TypeSpawn.Transform:
                        prefabSpawned = PoolManager.Spawn(prefab, ownerTransform, localPosition ,Quaternion.Euler(localRotation), localScale);
                        if (!setParent)
                        {
                            prefabSpawned.transform.parent = null;
                        }
                        UseRayCast(ownerTransform.position, ownerTransform.right, Mathf.Abs(localPosition.x), layerCastLimit,prefabSpawned.transform,ownerTransform);
                        break;
                    case TypeSpawn.RigidBody2D:
                        prefabSpawned = PoolManager.Spawn(prefab, ownerTransform, localPosition,Quaternion.Euler(localRotation), localScale);
                        
                        if (!setParent)
                        {
                            prefabSpawned.transform.parent = null;
                        }
                        UseRayCast(ownerTransform.position, ownerTransform.right, Mathf.Abs(localPosition.x), layerCastLimit,prefabSpawned.transform,ownerTransform);
                        Rigidbody2D rg = prefabSpawned.GetComponent<Rigidbody2D>();
                        if (rg)
                        {
                            var right = prefabSpawned.transform.right;
                            rg.AddForce(new Vector2(forceSpawn.x * right.x,forceSpawn.y *right.y));
                        }
                        break;
                    case TypeSpawn.Forward:
                        Vector3 localScaleCalculate = new Vector3(localScale.x * (ownerTransform.transform.right.x < 0 ? -1f : 1f),
                            localScale.y,
                            localScale.z);
                
                        Collider2D[] cols = null;
                
                        cols = Physics2D.OverlapCircleAll(ownerTransform.position, 100, layerCastLimit);
                        if (cols != null)
                        {
                            foreach (var col in cols)
                            {
                                if (col != null)
                                {
                                    Vector3 direction = col.transform.position - ownerTransform.position;
                                    Vector3 rightTransform = direction.normalized  * ownerTransform.localScale.x ;
                                    //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition, rightTransform, localScaleCalculate);
                                    prefabSpawned = PoolManager.Spawn(prefab, ownerTransform, localPosition, rightTransform, localScaleCalculate);
                                    break;
                                }
                            }
                        }
                        if (!setParent)
                        {
                            prefabSpawned.transform.parent = null;
                        }
                        PoolManager.Recycle(prefabSpawned, duration);
                        
                        break;
                }

                if (useDuration)
                {
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
            RaycastHit2D hit = Physics2D.Raycast(parent.position, new Vector2(1,0)* parent.localScale.x, Mathf.Abs(localPosition.x), this.layerCastLimit);
            if (hit.collider != null)
            {
                transform.position = new Vector3(hit.point.x, transform.position.y, transform.position.z); 
            }
        }
    }
#endregion

//#region EVENT CAST IMPACK
//
//public class EVENT_CAST_IMPACK : IComboEvent
//{
//    [FoldoutGroup("EVENT CAST IMPACK")] [ReadOnly] public int idEvent;
//
//    [FoldoutGroup("EVENT CAST IMPACK")] public float timeTriggerEvent;
//    
//    [FoldoutGroup("EVENT CAST IMPACK")] [PreviewField] public GameObject prefab;
//
//    [FoldoutGroup("EVENT CAST IMPACK")] [EnumToggleButtons] public TypeCast typeCast;
//    
//    [FoldoutGroup("EVENT CAST IMPACK")] public Vector3 position;
//    
//    [FoldoutGroup("EVENT CAST IMPACK")] public Vector3 rotation;
//    
//    [FoldoutGroup("EVENT CAST IMPACK")] public Vector3 scale;
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//
//    private GameObject spawned;
//    public void OnEventTrigger(GameEntity entity)
//    {
//        Transform owner = entity.stateMachineContainer.value.transform;
//        Vector3 offset = Vector3.zero;
//        Vector3 rot = Vector3.zero;
//        switch (typeCast)
//        {
//            case TypeCast.Forward:
//                spawned = PoolManager.Spawn(prefab, owner, position ,Quaternion.Euler(rotation), scale);
//                spawned.transform.parent = null;
//                break;
//            case TypeCast.Transform:
//                spawned = PoolManager.Spawn(prefab);
//                spawned.transform.position = owner.transform.position + position;
//                spawned.transform.rotation = Quaternion.Euler(rotation);
//                spawned.transform.localScale = scale;
//                break;
//        }
//    }
//
//    public void OnUpdateTrigger()
//    {
//
//    }
//
//    public void Recycle()
//    {
//        PoolManager.Recycle(spawned);
//    }
//}
//#endregion
public interface OnHitPhase                 
{                                            
                                                 
    int id { get; set; }                     
    float timeTrigger { get; }               
    void OnEventTrigger(GameEntity entity);  
    void OnUpdateTrigger();                  
    void Recycle();                          
}

public class ON_HIT_VFX : OnHitPhase
{
    [FoldoutGroup("HIT CAST COLLIDER")]              
    public int idEvent;
    [FoldoutGroup("HIT CAST COLLIDER")]     
    public float timeTriggerEvent;
    
    public int id { get { return idEvent; } set { idEvent = value; } }  
    public float timeTrigger { get { return timeTriggerEvent; } }       
    public void OnEventTrigger(GameEntity source, GameEntity target)
    {
        
    }

    public void OnEventTrigger(GameEntity entity)
    {
        
    }

    public void OnUpdateTrigger()
    {
        
    }

    public void Recycle()
    {
        
    }
}
//#region CAST ADD FORCE
//public class CastAddForce : IComboEvent
//{
//    [FoldoutGroup("CAST ADD FORCE")]
//    //[BoxGroup("Cast Projectile", true, true)]
//    [HideInEditorMode()]
//    public int idEvent;
//
//    [FoldoutGroup("CAST ADD FORCE")]
//    //[BoxGroup("Cast Projectile")]
//    [Range(0f, 5f)]
//    public float timeTriggerEvent;
//
//    [FoldoutGroup("CAST ADD FORCE")]
//    //[BoxGroup("Cast Projectile")]
//    public Vector3 force;
//
//
//
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//    private GameObject prefabSpawned;
//    public void OnEventTrigger(GameEntity entity)
//    {
//        Rigidbody2D baseRigidbody = entity.stateMachineContainer.value.componentManager.rgbody2D;
//        Transform baseTransform = entity.stateMachineContainer.value.transform;
//        
//        Vector3 CalculateForce = new Vector3(force.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
//            force.y,
//            force.z);
//        baseRigidbody.AddForce(CalculateForce);
//    }
//
//    public void Recycle()
//    {
//    }
//
//    public void OnUpdateTrigger()
//    {
//    }
//}
//#endregion

//#region CAST VFX
//public class CastVfxEvent : IComboEvent
//{
//    [FoldoutGroup("CAST VFX")]
//    [ReadOnly]
//    public int idEvent;
//
//    [FoldoutGroup("CAST VFX")]
//    public float timeTriggerEvent;
//    
//    [FoldoutGroup("CAST VFX")]
//    public float duration = 0.5f;
//
//    [FoldoutGroup("CAST VFX")]
//    [PreviewField]
//    public GameObject Prefab;
//
//    [FoldoutGroup("CAST VFX")]
//    public Vector3 Localosition;
//
//    [FoldoutGroup("CAST VFX")]
//    public Vector3 LocalRotation;
//
//    [FoldoutGroup("CAST VFX")]
//    public Vector3 LocalScale;
//
//    [FoldoutGroup("CAST VFX")]
//    public bool setParent = true;
//
//    [FoldoutGroup("CAST VFX")]
//    public bool recycleWhenFinishDuration = false;
//    
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//    private GameObject prefabSpawned;
//    public void OnEventTrigger(GameEntity entity)
//    {
//        if (Prefab)
//        {
//            //prefabSpawned = ObjectPool.Spawn(Prefab);
//            Transform baseTransform = entity.stateMachineContainer.value.transform;
//            prefabSpawned = PoolManager.Spawn(Prefab,baseTransform);
//            //prefabSpawned.transform.parent = baseTransform;
//            prefabSpawned.transform.localPosition = new Vector3(Localosition.x , Localosition.y , Localosition.z );
//            prefabSpawned.transform.localRotation = Quaternion.Euler(LocalRotation);
//            prefabSpawned.transform.localScale = LocalScale;
//            if (!setParent)
//            {
//                prefabSpawned.transform.parent = null;
//            }
//            PoolManager.Recycle(prefabSpawned,duration);
//            //ObjectPool.instance.Recycle(prefabSpawned, duration);
//            
//        }
//    }
//    public void Recycle()
//    {
//        if (recycleWhenFinishDuration)
//        {
//            if (prefabSpawned)
//            {
//                //ObjectPool.Recycle(prefabSpawned);
//                PoolManager.Recycle(prefabSpawned,duration);
//            }
//        }
//        else
//        {
//            if (prefabSpawned)
//                prefabSpawned.transform.parent = null;
//        }
//    }
//
//    public void OnUpdateTrigger()
//    {
//        
//    }
//}
//#endregion


//#region ENABLE COMPONENT
//public class EnableComponent : IComboEvent
//{
//    [FoldoutGroup("ENABLE COMPONENT")]
//    [ReadOnly]
//    public int idEvent;
//
//    [FoldoutGroup("ENABLE COMPONENT")]
//    [Range(0f, 5f)]
//    public float timeTriggerEvent;
//
//    [FoldoutGroup("ENABLE COMPONENT")]
//    public bool enable;
//
//    [FoldoutGroup("ENABLE COMPONENT")] 
//    public TypeComponent Component;
//    
//    
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//
//    public void OnEventTrigger(GameEntity entity)
//    {
//        switch (Component)
//        {
//            case TypeComponent.MeshRenderer:
//                MeshRenderer meshRenderer  = entity.stateMachineContainer.value.componentManager.meshRenderer ;
//                if (meshRenderer != null) meshRenderer.enabled = enable;
//                break;
//        }
//    }
//
//    public void OnUpdateTrigger()
//    {
//        
//    }
//
//    public void Recycle()
//    {
//    }
//}
//#endregion





//#region CAST PROJECTILE EVENT
//public class CastProjectile : IComboEvent
//{
//    [FoldoutGroup("PROJECTILE")]
//    [ReadOnly]
//    public int idEvent;
//
//    [FoldoutGroup("PROJECTILE")] 
//    public float timeTriggerEvent;
//    
//    [FoldoutGroup("PROJECTILE")]
//    public bool useDuration;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [ShowIf("useDuration")]
//    public float duration ;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [ShowIf("useDuration")]
//    public bool forceWhenFinishEvent;
//
//    [FoldoutGroup("PROJECTILE")]  
//    public bool setParent ;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [EnumToggleButtons]
//    public TypeSpawn typeSpawn;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [PreviewField]
//    public GameObject Prefab;
//
//    [FoldoutGroup("PROJECTILE")]
//    public Vector3 localPosition;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    public Vector3 LocalRotation;
//
//    [FoldoutGroup("PROJECTILE")] 
//    public Vector3 LocalScale = Vector3.one;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [ShowIf("typeSpawn", TypeSpawn.RigidBody2D)]   
//    public Vector2 ForceSpawn;
//    
//    [FoldoutGroup("PROJECTILE")] 
//    [HideReferenceObjectPicker]
//    public DamageInfoEvent damageInfoEvent = new DamageInfoEvent();
//
//    [FoldoutGroup("PROJECTILE")]     
//    public LayerMask LayerMaskLimitPosition ;
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//    private GameObject prefabSpawned;
//    public void OnEventTrigger(GameEntity entity)
//    {
//        
//        if (Prefab)
//        {
//            Transform baseTransform = entity.stateMachineContainer.value.transform;
//            switch (typeSpawn)
//            {
//                case TypeSpawn.Transform:
//                    //prefabSpawned = ObjectPool.Spawn(Prefab,baseTransform,localPosition,Quaternion.Euler(LocalRotation),LocalScale);
//                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,Quaternion.Euler(LocalRotation), LocalScale);
//                    if (!setParent)
//                    {
//                        prefabSpawned.transform.parent = null;
//                    }
//                    UseRayCast(prefabSpawned.transform, baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMaskLimitPosition);
//                    break;
//                
//                case TypeSpawn.RigidBody2D:
//                    //prefabSpawned = ObjectPool.Spawn(Prefab,baseTransform,localPosition,Quaternion.Euler(LocalRotation),LocalScale);
//                    prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,Quaternion.Euler(LocalRotation), LocalScale);
//                    if (!setParent)
//                    {
//                        prefabSpawned.transform.parent = null;
//                    }
//                    UseRayCast(prefabSpawned.transform, baseTransform.position, new Vector2(1,0)* baseTransform.localScale.x, Mathf.Abs(localPosition.x), LayerMaskLimitPosition);
//                    Rigidbody2D temp = prefabSpawned.GetComponent<Rigidbody2D>();
//                    if (temp)
//                    {
//                        var localScale = prefabSpawned.transform.localScale;
//                        temp.AddForce(new Vector2(ForceSpawn.x*localScale.x,ForceSpawn.y * localScale.y));
//                    }
//                    break;
//                
//                case TypeSpawn.Forward:
////                    Vector3 localScaleCalculate = new Vector3(LocalScale.x * (baseTransform.localScale.x < 0 ? -1f : 1f),
////                                                            LocalScale.y,
////                                                            LocalScale.z);
//                    Collider2D[] cols = Physics2D.OverlapCircleAll(baseTransform.position, 100, LayerMaskLimitPosition);
//                    if (cols != null)
//                    {
//                        foreach (var col in cols)
//                        {
//                            if (col != null)
//                            {
//                                Vector3 direction = col.transform.position - baseTransform.position;
//                                Vector3 rightTransform = direction.normalized  * baseTransform.localScale.x ;
//                                //prefabSpawned = ObjectPool.Spawn(Prefab, baseTransform, localPosition, rightTransform, LocalScale);
//                                prefabSpawned = PoolManager.Spawn(Prefab, baseTransform, localPosition ,rightTransform, LocalScale);
//                                break;
//                            }
//                        }
//                    }
//                    if (!setParent)
//                    {
//                        prefabSpawned.transform.parent = null;
//                    }
//
//                    if (useDuration)
//                    {
//                        //ObjectPool.instance.Recycle(prefabSpawned, duration);
//                        PoolManager.Recycle(prefabSpawned, duration);
//                    }
//
//                    break;
//                
//            }
//            
//            StateMachineController state = prefabSpawned.GetComponent<StateMachineController>();
//            ProjectileComponent prj = prefabSpawned.GetComponent<ProjectileComponent>();
//            if (state != null)
//            {
//                if (state.componentManager.entity != null)
//                {
//                    state.componentManager. damageInfoEvent = new DamageInfoEvent(damageInfoEvent);
//                    state.componentManager.entity.power.value = entity.power.value;
//                    
//                }
//            }
//            if (prj != null)
//            {
//                if (prj.entity != null)
//                {
//                    prj.entity.power.value = entity.power.value; 
//                    prj.projectileCollider.damageInfoEvent = new DamageInfoEvent(damageInfoEvent);
//                }
//
//            }
//
//            if (useDuration)
//            {
//                //ObjectPool.instance.Recycle(prefabSpawned, duration);
//                PoolManager.Recycle(prefabSpawned, duration);
//            }
//        }
//    }
//    public void Recycle()
//    {
//        if (useDuration)
//        {
//            if (forceWhenFinishEvent)
//            {
//                if (prefabSpawned)
//                {
//                    //ObjectPool.Recycle(prefabSpawned);
//                    PoolManager.Recycle(prefabSpawned);
//                }
//            }
//            else
//            {
//                if (prefabSpawned)
//                    prefabSpawned.transform.parent = null;
//            }
//        }
//    }
//
//    public void OnUpdateTrigger()
//    {
//        
//    }
//    void UseRayCast(Transform transform ,Vector2 fromPosition,Vector2 direction,float distance,int layerMask)
//    {
//        RaycastHit2D hit = Physics2D.Raycast(fromPosition, direction,distance, LayerMaskLimitPosition);
//        if (hit.collider != null)
//        {
//            var position = transform.position;
//            position = new Vector3(hit.point.x,position.y,position.z);
//            transform.position = position;
//        }
//    }
//}
//#endregion



//#region CAMERA TARGET
//public class CameraTarget : IComboEvent
//{
//    [FoldoutGroup("CAMERA TARGET")]
//    [ReadOnly]
//    public int idEvent;
//
//    [FoldoutGroup("CAMERA TARGET")] 
//    public float timeTriggerEvent;
//    
//    [FoldoutGroup("CAMERA TARGET")] 
//    public Vector2 offset;
//
//    [FoldoutGroup("CAMERA TARGET")]
//    public bool mainTarget;
//
//    
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//    
//    public void OnEventTrigger(GameEntity entity)
//    {
//        Transform baseTransform = entity.stateMachineContainer.value.transform;
//        CameraController.instance.AddTarget(baseTransform);
//        CameraController.instance.FollowTarget(baseTransform,offset);
//        if(mainTarget)
//            CameraController.instance.SetMainTarget(baseTransform,offset);
//    }
//    public void Recycle()
//    {
//        CameraController.instance.RestoneMainTarget();
//    }
//
//    public void OnUpdateTrigger()
//    {
//        
//    }
//}
//#endregion

//#region REMOVE CAMERA TARGET
//public class RemoveCameraTarget : IComboEvent
//{
//    [FoldoutGroup("CAMERA TARGET")]
//    [ReadOnly]
//    public int idEvent;
//
//    [FoldoutGroup("CAMERA TARGET")] 
//    public float timeTriggerEvent;
//    
////    [FoldoutGroup("CAMERA TARGET")] 
////    public Vector2 offset;
////
////    [FoldoutGroup("CAMERA TARGET")]
////    public bool mainTarget;
//
//    
//    public int id { get { return idEvent; } set { idEvent = value; } }
//    public float timeTrigger { get { return timeTriggerEvent; } }
//    
//    public void OnEventTrigger(GameEntity entity)
//    {
//        Transform baseTransform = entity.stateMachineContainer.value.transform;
//        CameraController.instance.RemoveTarget(baseTransform);
//    }
//    public void Recycle()
//    {
//    }
//
//    public void OnUpdateTrigger()
//    {
//        
//    }
//}
//#endregion


}
