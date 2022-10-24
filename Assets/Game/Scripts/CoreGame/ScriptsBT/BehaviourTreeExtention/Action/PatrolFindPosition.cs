using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class PatrolFindPosition : Action
    {
//        public SharedComponentManager component;
        public SharedVector2 patrolTargetPosition;
        public SharedVector2 patrolLeftLimit;
        public SharedVector2 patrolRightLimit;
        public override void OnStart()
        {
            base.OnStart();
            float rX = Random.Range(patrolLeftLimit.Value.x , patrolRightLimit.Value.x );
            float rY = Random.Range(patrolLeftLimit.Value.y , patrolLeftLimit.Value.y );
            patrolTargetPosition.Value = new Vector2(rX,rY);
        }
    }

}

