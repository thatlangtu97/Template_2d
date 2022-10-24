using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Core.AI
{
    [TaskCategory("Extension")]
    public class CheckRange : Conditional
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
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failure;
                }
            }
            else
            {
                return TaskStatus.Failure;
            }


        }
    }

}