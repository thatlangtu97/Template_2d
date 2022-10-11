using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class PatrolFindSpawnPosition : Action
    {
        public SharedComponentManager component;
        public SharedVector2 spawnPosition;
        public bool spawnGround;
        public override void OnStart()
        {
            base.OnStart();
            

        }

        public override TaskStatus OnUpdate()
        {
            if (spawnGround)
            {
                if (component.Value.IsGround)
                {
                    spawnPosition.Value = component.Value.transform.position;
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Running;
                }
            }
            else
            {
                spawnPosition.Value = component.Value.transform.position;
                return TaskStatus.Success;
            }
        }
    }
   

}
