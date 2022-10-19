using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class RandomNextAction : Action
    {
        public SharedActionData currentAction;
        public override void OnStart()
        {
            base.OnStart();
            if (currentAction.Value.isDone)
            {
                currentAction.Value.index =currentAction.Value.listAction[Random.Range(0, currentAction.Value.listAction.Count)];
                currentAction.Value.isDone = false;
            }
        }
    }


}
