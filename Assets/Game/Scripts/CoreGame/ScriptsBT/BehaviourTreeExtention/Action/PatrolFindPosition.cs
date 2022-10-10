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
        public SharedVector2 spawnPosition;
        public Vector2 range;
        public override void OnStart()
        {
            base.OnStart();
//            float rX = Random.Range(component.Value.transform.position.x - range.x, component.Value.transform.position.x + range.x);
//            float rY = Random.Range(component.Value.transform.position.y - range.y, component.Value.transform.position.y + range.y);
            float rX = Random.Range(spawnPosition.Value.x - range.x, spawnPosition.Value.x + range.x);
            float rY = Random.Range(spawnPosition.Value.y - range.y, spawnPosition.Value.y + range.y);
            patrolTargetPosition.Value = new Vector2(rX,rY);
        }
    }

}

