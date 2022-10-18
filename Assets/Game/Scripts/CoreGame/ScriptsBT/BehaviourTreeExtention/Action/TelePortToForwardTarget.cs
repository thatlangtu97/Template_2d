using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class TelePortToForwardTarget : Action
    {
        public SharedComponentManager component;
        public SharedTransform target;
        public Vector3 foward;

        public override void OnStart()
        {
            base.OnStart();
            component.Value.transform.position = target.Value.position + new Vector3(target.Value.right.x * foward.x , target.Value.right.y * foward.y);
        }
    }


}
