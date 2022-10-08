using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveState", menuName = "CoreGame/State/MoveState")]
    public class MoveState : State
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
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.vectorMove.x * controller.componentManager.maxSpeedMove,controller.componentManager.rgbody2D.velocity.y);
            if (controller.componentManager.vectorMove == Vector2.zero)
            {
                controller.ChangeState(NameState.IdleState);
            }
            else
            {
                controller.transform.right = new Vector3(controller.componentManager.vectorMove.x,0);
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
        
        public override void OnInputJump()
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
    }
}

