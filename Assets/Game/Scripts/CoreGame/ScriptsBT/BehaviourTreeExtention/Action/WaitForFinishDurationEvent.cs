using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.GamePlay;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class WaitForFinishDurationEvent : Action
    {
        public SharedComponentManager component;
        public NameState currentState;
        public override void OnStart()
        {
            base.OnStart();
            currentState = component.Value.stateMachine.currentNameState;
        }

        public override TaskStatus OnUpdate()
        {
            if (component.Value.stateMachine.currentNameState != currentState)
            {
                return TaskStatus.Success;
            }
            else
            {
                if (component.Value.stateMachine.currentState.IsFinishDuration)
                {
                    return TaskStatus.Success;
                }
                
                return TaskStatus.Running;
            }
        }
    }

}
