using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "FallingState", menuName = "CoreGame/State/FallingState")]
    public class FallingState : State
    {
        private EventCollection currentState;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.Rotate();
            controller.componentManager.rgbody2D.velocity= new Vector2( controller.componentManager.vectorMove.x * controller.componentManager.maxSpeedMove, controller.componentManager.rgbody2D.velocity.y );
            if (controller.componentManager.IsGround)
            {
//                if (controller.componentManager.vectorMove != Vector2.zero)
//                {
//                    controller.ChangeState(NameState.MoveState);
//                    return;
//                }
                controller.ChangeState(NameState.LandingState);
            }
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        public override void OnInputDash()
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
        
        public override void OnInputAttack()
        {
            base.OnInputAttack();
            controller.ChangeState(NameState.AirAttackState);
        }
        
//        public override void OnInputJump()
//        {
//            base.OnInputJump();
//            controller.ChangeState(NameState.JumpState);
//
//        }
    }
}

