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
        public override void OnStart()
        {
            base.OnStart();
            spawnPosition.Value = component.Value.transform.position;

        }
    }
   

}
