using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class PatrolStartMove : Action
    {
        public SharedComponentManager component;
        public SharedVector2 patrolTargetPosition;

        public override void OnStart()
        {
            base.OnStart();
            component.Value.vectorMove = (patrolTargetPosition.Value - (Vector2)component.Value.transform.position).normalized;
        }
    }

}

