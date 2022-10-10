using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Core.AI
{
    [TaskCategory("Extension")]
    public class LookAtTarget : Action
    {
        public SharedComponentManager component;

        public SharedTransform target;

        public override void OnStart()
        {
            base.OnStart();
            
            if (target.Value)
            {
                Vector2 right = (target.Value.transform.position - component.Value.transform.position).normalized;
                component.Value.transform.right = new Vector3(right.x,component.Value.transform.right.y);
            }
        }
    }

}
