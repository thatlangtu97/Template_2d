using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class StartMove : Action
    {
        public SharedComponentManager component;

        public SharedTransform target;
        public override void OnStart()
        {
            base.OnStart();
            component.Value.vectorMove = (target.Value.transform.position - component.Value.transform.position).normalized;
        }
    }


}
