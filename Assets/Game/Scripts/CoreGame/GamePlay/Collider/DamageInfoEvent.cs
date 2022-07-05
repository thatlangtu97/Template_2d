using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.GamePlay
{
    [System.Serializable]
    public class DamageInfoEvent
    {
        public float damageScale;
        [EnumToggleButtons]
        public PowerCollider powerCollider;
        public Vector2 forcePower;

        public DamageInfoEvent(DamageInfoEvent clone)
        {
            this.damageScale = clone.damageScale;
            this.powerCollider = clone.powerCollider;
            this.forcePower = clone.forcePower;
        }
        public DamageInfoEvent(){}
    }
}

