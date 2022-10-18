using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class PatrolCheck : Conditional
    {
        public SharedComponentManager component;
        public SharedVector2 patrolTargetPosition;
        public float rangeStop;
        public override TaskStatus OnUpdate()
        {

            if (Vector2.Distance(component.Value.transform.position, patrolTargetPosition.Value) <= rangeStop)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }

        }
    }

}

