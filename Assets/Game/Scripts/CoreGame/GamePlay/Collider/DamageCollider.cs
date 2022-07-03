﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

namespace Core.GamePlay
{
    public class DamageCollider : MonoBehaviour
    {
        public BoxCollider2D boxCollider2D;
        public CircleCollider2D circleCollider2D;
        public DamageInfoEvent damageInfoEvent;
        public int damageProperties;
        public GameEntity myEntity;
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            void Action()
            {
                other.GetComponent<Rigidbody2D>().AddForceAtPosition(damageInfoEvent.forcePower * transform.localScale.x, other.transform.position);
            }
            Vector2 direction = (other.transform.position - transform.position).normalized;
            
            DamageInfoSend damageInfoSend = new DamageInfoSend(damageInfoEvent, damageProperties, Action);
            DealDmgManager.DealDamage(other, myEntity, damageInfoSend);
            Debug.Log("damage by Object");
        }

        public void SetCollider(ColliderCast typeCollider, Vector2 sizeBox , int damageProperties , DamageInfoEvent damageInfoEvent, GameEntity myEntity)
        {
            switch (typeCollider)
            {
                case ColliderCast.Box:
                    this.damageProperties = damageProperties;
                    this.damageInfoEvent= new DamageInfoEvent(damageInfoEvent);
                    boxCollider2D.size = sizeBox;
                    boxCollider2D.enabled = true;
                    circleCollider2D.enabled = false;
                    this.myEntity = myEntity;
                    break;
            }
        }
        public void SetCollider(ColliderCast typeCollider, float radius,  int damageProperties , DamageInfoEvent damageInfoEvent, GameEntity myEntity)
        {
            switch (typeCollider)
            {
                case ColliderCast.Circle:
                    this.damageProperties = damageProperties;
                    this.damageInfoEvent= new DamageInfoEvent(damageInfoEvent);
                    circleCollider2D.radius = radius;
                    boxCollider2D.enabled = false;
                    circleCollider2D.enabled = true;
                    this.myEntity = myEntity;
                    break;
            }
        }

        public void Recycle()
        {
            boxCollider2D.enabled = false;
            circleCollider2D.enabled = false;
        }
    }

}

