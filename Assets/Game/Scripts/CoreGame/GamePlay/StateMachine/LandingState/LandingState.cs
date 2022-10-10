using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "LandingState", menuName = "CoreGame/State/LandingState")]
    public class LandingState : State
    {
        private EventCollection currentState;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger > currentState.durationAnimation)
            {
                
                controller.ChangeState(NameState.IdleState);
               

            }
            if (controller.componentManager.vectorMove!=Vector2.zero)
            {
                controller.ChangeState(NameState.MoveState);
                return;
            }
            
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        public override void OnInputAttack()
        {
            base.OnInputAttack();
            controller.ChangeState(NameState.AttackState);
        }

        public override void OnInputMove()
        {
            base.OnInputMove();
            if (timeTrigger > currentState.durationAnimation)
                controller.ChangeState(NameState.MoveState);
        }

        public override void OnInputJump()
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
        public override void OnInputDash()
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
        public override void OnInputSkill(int idSkill)
        {
            base.OnInputSkill(idSkill);
            controller.ChangeState(NameState.SkillState,idSkill);
        }
        public override void OnInputCounter()
        {
            base.OnInputCounter();
            controller.ChangeState(NameState.CounterState);
        }
    }
}
