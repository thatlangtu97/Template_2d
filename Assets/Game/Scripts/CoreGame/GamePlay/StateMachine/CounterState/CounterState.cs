using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "CounterState", menuName = "CoreGame/State/CounterState")]
    public class CounterState : State
    {
        private EventCollection currentState;
        public bool ListenerEvent;
        public override void EnterState()
        {
            base.EnterState();
            if (!ListenerEvent)
            {
                this.RegisterListener(EventID.TAKE_DAMAGE, (sender, param) =>Counter());
                ListenerEvent = true;
            }
            idState = 0;
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
        }

        void Counter()
        {
            if (controller.currentNameState == NameState.CounterState)
            {
                Debug.Log("Counter Success");
            }
        }
        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.rgbody2D.velocity = new Vector2(currentState.curveX.Evaluate(timeTrigger) * controller.transform.right.x,currentState.curveY.Evaluate(timeTrigger) + controller.componentManager.rgbody2D.velocity.y );
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
