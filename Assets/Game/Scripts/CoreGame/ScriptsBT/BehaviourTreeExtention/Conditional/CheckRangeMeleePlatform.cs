using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    
    [TaskCategory("Extension")]
    public class CheckRangeMeleePlatform : Conditional
    {
        public SharedComponentManager component;
        public SharedVector2 patrolLeftLimit;
        public SharedVector2 patrolRightLimit;
        public SharedTransform target;
        public LayerMask layerPlatform;
        public bool inPlatform = false;
        public float distanceCheckGround;
        public override TaskStatus OnUpdate()
        {
            
            if (target.Value == null)
            {
//                if (hit.collider == null)
//                    if (inPlatform)
//                    {
//                        Owner.DisableBehavior();
//                        Owner.EnableBehavior();
//                        inPlatform = false;
//                    }
                return TaskStatus.Failure;
            }
            RaycastHit2D hit =  Physics2D.Raycast(component.Value.transform.position , Vector2.down, distanceCheckGround, layerPlatform);

            if (hit.collider != null)
            {
                inPlatform = true;
                
                if (target.Value.transform.position.x <= patrolLeftLimit.Value.x || target.Value.transform.position.x >= patrolRightLimit.Value.x )
                {
                    return TaskStatus.Failure;
                }
                else
                {
                    return TaskStatus.Success;
                }
            }
            else
            {
                if (inPlatform)
                {
                    Owner.DisableBehavior();
                    Owner.EnableBehavior();
                    inPlatform = false;
                }
                return TaskStatus.Success;
            }
        }
    }

}
