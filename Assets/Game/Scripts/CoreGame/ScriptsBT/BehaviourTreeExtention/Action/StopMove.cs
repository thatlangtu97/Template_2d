using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


namespace Core.AI
{
    [TaskCategory("Extension")]
    public class StopMove : Action
    {
        public SharedComponentManager component;
        public override void OnStart()
        {
            base.OnStart();
            component.Value.vectorMove = Vector2.zero;
        }
    }

}
