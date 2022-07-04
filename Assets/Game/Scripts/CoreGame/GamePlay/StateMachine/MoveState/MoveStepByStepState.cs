using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveStepByStepState", menuName = "CoreGame/State/MoveStepByStepState")]
    public class MoveStepByStepState : State
    {
        public AnimationCurve curveX;
        private EventCollection currentEvent;
        public override void EnterState()
        {
            base.EnterState();
            controller.PlayAnim(currentEvent.NameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger >= currentEvent.durationAnimation)
            {
                ResetEvent();
                controller.PlayAnim(currentEvent.NameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
            }
        }
        public override void ExitState()
        {
            base.ExitState();
        }
    }
}

