using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Core.GamePlay;
using UnityEngine;

namespace Core.AI
{
    [TaskCategory("Extension")]
    public class ChangeInputState : Action
    {
        public SharedComponentManager componentManager;
        public NameState nameState;
        public int idState;
        public bool forceChangeState;
        public override void OnStart()
        {
            if(!forceChangeState)
                switch (nameState)
                {
                    case NameState.AttackState:
                        componentManager.Value.stateMachine.OnInputAttack();
                        break;
                    case NameState.DashState:
                        componentManager.Value.stateMachine.OnInputDash();
                      break;
                    case NameState.SkillState:
                        componentManager.Value.stateMachine.OnInputSkill(idState);
                        break;
                    case NameState.MoveState:
                        componentManager.Value.stateMachine.OnInputMove();
                        break;
                }
            else
            {
                componentManager.Value.stateMachine.ChangeState(nameState,idState,true);
            }
            base.OnStart();
        }
    }
}