using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
    public class AttackState : State
    {
        public List<float> timeBuffers = new List<float>();
        private EventCollection currentEvent;
        private float duration;
        private bool buffer;
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
            CastSkill();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger >= duration)
            {
                if (buffer)
                {
                    idState = (idState + 1) % eventCollectionData.Count;
                    CastSkill();
                }
                else
                {
                    controller.ChangeState(NameState.IdleState);
                }
                
            }

            float velocityX = currentEvent.curveX.Evaluate(timeTrigger);
            float velocityY = currentEvent.curveY.Evaluate(timeTrigger);
            Vector2 velocity = controller.componentManager.rgbody2D.velocity;
            controller.componentManager.rgbody2D.velocity = new Vector2(velocityX + velocity.x , velocityY + velocity.y);
        }
        public override void ExitState()
        {
            base.ExitState();
        }

        void CastSkill()
        {
            currentEvent = eventCollectionData[idState];
            controller.PlayAnim(currentEvent.NameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
            duration = currentEvent.durationAnimation;
            ResetEvent();
            buffer = false;

        }

        public override void OnInputAttack()
        {
            base.OnInputAttack();
            buffer = true;
        }
    }

}

