using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskDescription(@"bộ đếm time theo time scale của Component manager với time scale của Unity")]
    [TaskCategory("Extension")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class WaitFrameTask : Action
    {
        public int frame;
        public int countFrame;
        public override void OnStart()
        {
            base.OnStart();
            countFrame = 0;
        }

        public override TaskStatus OnUpdate()
        {
       
            if (countFrame >=frame)
            {
                return TaskStatus.Success;
            }
            countFrame += 1;
            return TaskStatus.Running;
        }
    }
}

