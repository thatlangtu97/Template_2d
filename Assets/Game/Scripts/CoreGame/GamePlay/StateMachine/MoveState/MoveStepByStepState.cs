using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveStepByStepState", menuName = "CoreGame/State/MoveStepByStepState")]
    public class MoveStepByStepState : State
    {
        private EventCollection currentEvent;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            CastSkill();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger >= currentEvent.duration)
            {
                CastSkill();
            }
            float velocityX = currentEvent.curveVelocityX.Evaluate(timeTrigger);
            float velocityY = currentEvent.curveVelocityY.Evaluate(timeTrigger);
            controller.componentManager.rgbody2D.velocity = new Vector2( velocityX , velocityY);
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        void CastSkill()
        {
            currentEvent = eventCollectionData[idState];
            controller.PlayAnim(currentEvent.nameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
            duration = currentEvent.duration;
            ResetEvent();

        }
    }
}

