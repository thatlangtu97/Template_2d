using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;
namespace Core.AI
{
    [TaskCategory("Extension")]
    public class MoveForwark : Action
    {
        public SharedComponentManager componentManager;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.ChangeState(NameState.MoveState);
        }
    }
}
