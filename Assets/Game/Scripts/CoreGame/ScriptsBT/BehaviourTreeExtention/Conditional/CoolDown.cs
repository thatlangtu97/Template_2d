using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class CoolDown : Conditional
    {
        public float coolDownTime;
        public float timeTrigger;

        public override TaskStatus OnUpdate()
        {
            timeTrigger += Time.deltaTime;

            if (timeTrigger >= coolDownTime)
            {
                timeTrigger = 0;
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }


}
