using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "CounteredState", menuName = "CoreGame/State/CounteredState")]
    public class CounteredState : State
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
            controller.componentManager.rgbody2D.velocity = new Vector2(currentState.curveX.Evaluate(timeTrigger) * controller.transform.right.x , controller.componentManager.rgbody2D.velocity.y );
            
            if (timeTrigger >= currentState.durationAnimation)
            {
                if (controller.componentManager.IsGround)
                {
                    if (controller.componentManager.vectorMove != Vector2.zero)
                    {
                        controller.ChangeState(NameState.MoveState);
                        return;
                    }

                    controller.ChangeState(NameState.IdleState);
                }
                else
                {
                    controller.ChangeState(NameState.FallingState);
                }
            }

        }
        

        public override void ExitState()
        {
            base.ExitState();
        }
    }

}
