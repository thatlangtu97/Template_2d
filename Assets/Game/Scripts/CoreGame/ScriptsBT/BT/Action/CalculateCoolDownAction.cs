using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace CoreBT
{
    [TaskCategory("Extension")]
    public class CalculateCoolDownAction : Action
    {
        public SharedActionData ActionData;
        public  TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}

