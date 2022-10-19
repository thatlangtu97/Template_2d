using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class CheckAction : Conditional
    {
        public SharedActionData currentAction;
        public int idAction;
        public override TaskStatus OnUpdate()
        {
            if (currentAction.Value.index == idAction)
            {
                if (currentAction.Value.isDone == false)
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
