using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEngine.Serialization;

public class ProjectileCollider : MonoBehaviour
{
    public DamageInfoEvent damageInfoEvent;
    public ProjectileComponent component;
    public Collider2D colliderProjectile;
    public float delayEnableCollider;

    private float timeTrigger = 0;
    public virtual void Awake()
    {
        if(!component)
            component = GetComponent<ProjectileComponent>();
        colliderProjectile = GetComponent<Collider2D>();
    }
    
    public void OnEnable()
    {
        timeTrigger = 0;
        colliderProjectile.enabled = false;
    }

    public void UpdateCollider()
    {
        timeTrigger += Time.deltaTime;
        if (timeTrigger > delayEnableCollider)
        {
            colliderProjectile.enabled = true;
        }
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        ComponentManager componentManager =ComponentManagerUtils.GetComponentByInstanceId(other.gameObject.GetInstanceID());
        void Action()
        {
            componentManager.rgbody2D.AddForceAtPosition(damageInfoEvent.forcePower * transform.localScale.x, other.transform.position);
        }

        DamageInfoSend damageInfoSend = new DamageInfoSend(damageInfoEvent, component.entity.power.value, Action);
        DealDmgManager.DealDamage(componentManager.entity, component.entity, damageInfoSend);
        PoolManager.Recycle(this.gameObject);
    }
}
