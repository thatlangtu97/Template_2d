using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "CounterState", menuName = "CoreGame/State/CounterState")]
    public class CounterState : State
    {
        private EventCollection currentState;

        public override void InitState(StateMachineController controller)
        {
            base.InitState(controller);
            this.RegisterListener(EventID.TAKE_DAMAGE, (sender, param) =>Counter( param));
        }

        public override void EnterState()
        {
            base.EnterState();
            //controller.componentManager.AddImunes(new List<Immune>(){Immune.HIT});
            idState = 0;
            controller.componentManager.AddBufferImmunes(Immune.BLOCK);
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
        }

        void Counter(object obj)
        {
            Debug.Log("Counter Enter");
            StateMachineController smc= obj as StateMachineController;
            if(smc == controller)
                if (controller.currentNameState == NameState.CounterState)
                {
                    
                    controller.ChangeState(NameState.AttackState);
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
            controller.componentManager.RemoveBufferImmunes(Immune.BLOCK);
        }
        
    }
}
