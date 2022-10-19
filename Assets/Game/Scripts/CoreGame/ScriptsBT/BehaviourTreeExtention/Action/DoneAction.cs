using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class DoneAction : Action
    {
        public SharedActionData currentAction;

        public override void OnStart()
        {
            base.OnStart();
            currentAction.Value.isDone = true;
        }
    }


}
