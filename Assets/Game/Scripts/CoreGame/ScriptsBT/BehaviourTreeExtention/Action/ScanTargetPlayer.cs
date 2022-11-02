using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class ScanTargetPlayer : Action
    {
        public SharedTransform target;
        public int frameSpaceScan;
        private int countFrame;
        public override TaskStatus OnUpdate()
        {
            if (!target.Value)
            {
                countFrame += 1;
                if (countFrame >= frameSpaceScan)
                {
                    target.Value = Contexts.sharedInstance.game.playerFlagEntity.stateMachineContainer.value.transform;
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Running;
                }
            }

            return TaskStatus.Running;
        }
    }


}
