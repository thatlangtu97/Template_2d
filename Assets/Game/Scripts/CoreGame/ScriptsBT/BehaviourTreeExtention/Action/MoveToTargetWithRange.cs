using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class MoveToTargetWithRange : Action
    {
        public SharedComponentManager component;
        public SharedTransform target;
        public SharedFloat range;
        public float rangeSuccess;


        public override TaskStatus OnUpdate()
        {
            if (target.Value)
            {
                range.Value = Vector2.Distance(component.Value.transform.position, target.Value.transform.position);
                if (range.Value <= rangeSuccess)
                {
                    component.Value.rgbody2D.velocity=Vector2.zero;
                    return TaskStatus.Success;
                }
                else
                {
                    component.Value.rgbody2D.velocity =
                        (target.Value.position - component.Value.transform.position).normalized *
                        component.Value.maxSpeedMove;
                    return TaskStatus.Running;
                }
            }
            else
            {
                component.Value.rgbody2D.velocity=Vector2.zero;
                return TaskStatus.Success;
            }
            
        }
    }
}

