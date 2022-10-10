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
        public override void OnStart()
        {
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
            base.OnStart();
        }
    }
}