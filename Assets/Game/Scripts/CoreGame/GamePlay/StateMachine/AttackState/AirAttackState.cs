using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "AirAttackState", menuName = "CoreGame/State/AirAttackState")]
    public class AirAttackState : State
    {
        private EventCollection currentState;
        public override void EnterState()
        {
            base.EnterState();
            Cast();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.Rotate();
            controller.componentManager.rgbody2D.velocity= new Vector2(controller.componentManager.vectorMove.x * controller.componentManager.maxSpeedMove, controller.componentManager.rgbody2D.velocity.y);
            if (timeTrigger > currentState.durationAnimation)
            {
                if (controller.componentManager.IsGround)
                {
                    controller.ChangeState(NameState.LandingState);
                    return;
                }
                else
                {
                    controller.ChangeState(NameState.FallingState);
                }
                
            }
            else
            {
                if (controller.componentManager.IsGround)
                {
                    controller.ChangeState(NameState.LandingState);
                    return;
                }
            }

        }

        public override void ExitState()
        {
            base.ExitState();
        }
        
        void Cast()
        {
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            ResetEvent();
        }
    }
}