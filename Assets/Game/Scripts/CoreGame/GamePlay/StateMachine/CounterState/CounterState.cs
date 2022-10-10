using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "CounterState", menuName = "CoreGame/State/CounterState")]
    public class CounterState : State
    {
        private EventCollection currentState;
        private float duration;
        private bool successCounter, counted;
        public override void InitState(StateMachineController controller)
        {
            base.InitState(controller);
            this.RegisterListener(EventID.TAKE_DAMAGE, (sender, param) => Counter(param));
        }

        public override void EnterState()
        {
            base.EnterState();
            idState = 0;
            controller.componentManager.AddImunes(Immune.BLOCK);
            successCounter = false;
            counted = false;
            Cast();
        }

        void Counter(object obj)
        {
            StateMachineController smc= obj as StateMachineController;
            if(smc == controller)
                if (controller.currentNameState == NameState.CounterState)
                {
                    successCounter = true;
                    
                }
        }
        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.rgbody2D.velocity = new Vector2(currentState.curveX.Evaluate(timeTrigger) * controller.transform.right.x,currentState.curveY.Evaluate(timeTrigger) + controller.componentManager.rgbody2D.velocity.y );
            if (timeTrigger >= duration)
            {
                if (!successCounter || counted )
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
                else
                {
                    if (idState + 1 < eventCollectionData.Count)
                    {
                        idState += 1;
                        counted = true;
                        Cast();
                    }
                }
            }
        }
        public override void ExitState()
        {
            base.ExitState();
            controller.componentManager.RemoveImmunes(Immune.BLOCK);
        }
        void Cast()
        {
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            ResetEvent();
        }
    }
}
