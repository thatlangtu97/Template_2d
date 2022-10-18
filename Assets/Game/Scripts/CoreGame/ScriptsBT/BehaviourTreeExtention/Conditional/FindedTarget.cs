using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class FindedTarget : Conditional
    {
        public SharedTransform target;
        public Transform bufferTarget;
        public Transform targetTest;
        public override TaskStatus OnUpdate()
        {
            targetTest = target.Value;
            if (bufferTarget == null)
            {
                if (target.Value != null)
                {
                    bufferTarget = target.Value;
                    return TaskStatus.Success;
                }
            }

            if (target.Value == null)
            {
                bufferTarget = null;
                return TaskStatus.Failure;
            }
            
            
            
            
            return TaskStatus.Failure;
        }
    }


}
