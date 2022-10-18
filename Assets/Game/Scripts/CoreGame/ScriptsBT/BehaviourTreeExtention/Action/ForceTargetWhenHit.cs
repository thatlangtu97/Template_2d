using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.GamePlay;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    
    public class ForceTargetWhenHit : Action
    {
        public SharedComponentManager component;
        public SharedTransform target;
        public override void OnAwake()
        {
            base.OnAwake();
            this.RegisterListener(EventID.TAKE_DAMAGE, (sender, param) => Hit(param));
        }

        public void Hit(object obj)
        {
            DataDamageTake data = obj as DataDamageTake;
            if (data.source != component.Value.stateMachine) return;
            if (target.Value == null)
            {
                target.Value = data.target.transform;
                Vector2 right = (data.target.transform.position - component.Value.transform.position).normalized;
                component.Value.vectorMove = right;
            }   
        }
        
    }
}

