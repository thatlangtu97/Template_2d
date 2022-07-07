using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

namespace Core.GamePlay 
{
    [CreateAssetMenu(fileName = "ParryState", menuName = "CoreGame/State/ParryState")]
    public class ParryState : State
    {
        private EventCollection currentEvent;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            currentEvent = eventCollectionData[idState];
            duration = currentEvent.duration;
            controller.PlayAnim(currentEvent.nameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
            controller.componentManager.isParry = true;
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger > duration)
            {
                if (controller.componentManager.isParry == false)
                { 
                    controller.ChangeState(NameState.IdleState);
                }
            }
            
        }
        public override void ExitState()
        {
            base.ExitState();
            controller.componentManager.isParry = false;
        }
    }

}

