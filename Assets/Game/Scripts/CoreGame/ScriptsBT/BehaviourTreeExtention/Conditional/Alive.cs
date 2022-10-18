using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class Alive : Conditional
    {
        public SharedComponentManager component;
        
        public override TaskStatus OnUpdate()
        {
            if (component.Value.entity == null)
            {
                return TaskStatus.Failure;
            }
            
            if (component.Value.entity.health.health > 0)
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

