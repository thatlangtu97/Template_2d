using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "CoreGame/State/IdleState")]
    public class IdleState : State
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
    } 
}

