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
        public float range;
        public float rangeSuccess;
        public float speed=1f;


        public override TaskStatus OnUpdate()
        {
            if (target.Value)
            {
                range = Mathf.Abs(component.Value.transform.position.x-  target.Value.transform.position.x);
                if (range <= rangeSuccess)
                {
                    component.Value.rgbody2D.velocity=Vector2.zero;
                    return TaskStatus.Success;
                }
                else
                {
                    component.Value.rgbody2D.velocity = new Vector2(
                        target.Value.position.x-component.Value.transform.position.x,0f
                    ).normalized * component.Value.maxSpeedMove * speed;
//                        (target.Value.position - component.Value.transform.position).normalized *
//                        component.Value.maxSpeedMove;
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

