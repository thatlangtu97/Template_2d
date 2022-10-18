using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "SkillState", menuName = "CoreGame/State/SkillState")]
    public class SkillState : State
    {
        public List<float> timeBuffers = new List<float>();
        private EventCollection currentState;
        private float duration;
        protected override void OnBeforeSerialize()
        {
            if (eventCollectionData == null) return;
            if (eventCollectionData.Count==0 ) return;

            if (timeBuffers.Count < eventCollectionData.Count)
            {
                timeBuffers.Add(0f);
            }
            else
            {
                if (timeBuffers.Count > eventCollectionData.Count)
                {
                    timeBuffers.RemoveAt(timeBuffers.Count-1);
                }
            }
        }
        public override void EnterState()
        {
            base.EnterState();
            idState = 0;
            Cast();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            controller.componentManager.rgbody2D.velocity = new Vector2(currentState.curveX.Evaluate(timeTrigger) * controller.transform.right.x,currentState.curveY.Evaluate(timeTrigger) + controller.componentManager.rgbody2D.velocity.y );
            if (timeTrigger >= duration)
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

        void Cast()
        {
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            ResetTimeTrigger();
        }
    }
    
}

