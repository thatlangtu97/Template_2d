﻿using BehaviorDesigner.Runtime.Tasks;
using Core.GamePlay;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class ChangeState : Action
    {
        public SharedComponentManager componentManager;
        public NameState nameState;
        public int idState;
        public bool forceChangeState;
        public override void OnStart()
        {
            base.OnStart();
            componentManager.Value.stateMachine.ChangeState(nameState, idState, forceChangeState);

        }
    }
}