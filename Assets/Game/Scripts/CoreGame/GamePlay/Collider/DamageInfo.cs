using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    [System.Serializable]
    public class DamageInfoSend
    {
        public DamageInfoEvent damageInfoEvent;
        public int damageProperties;
        public Action action;
        public DamageInfoSend(){}
        public DamageInfoSend(DamageInfoEvent damageInfoEvent , int damageProperties, Action action)
        {
            this.damageInfoEvent = damageInfoEvent;
            this.damageProperties = damageProperties;
            this.action = action;
        }
    }

}

