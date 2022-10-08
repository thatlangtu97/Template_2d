using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
    public class AttackState : State
    {
        public List<float> timeBuffers = new List<float>();
        private EventCollection currentState;
        private float duration;
        private bool bufferAttack;
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
                if (bufferAttack)
                {
                    idState = (idState+1)%eventCollectionData.Count;
                    Cast();
                }
                else
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
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        
        public override void OnInputAttack()
        {
            base.OnInputAttack();
            if (idState >= eventCollectionData.Count - 1)
            {
                bufferAttack = false;
                return;
            }
            bufferAttack = true;
        }

        void Cast()
        {
            controller.componentManager.Rotate();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            bufferAttack = false;
            ResetEvent();
            this.PostEvent(EventID.TAKE_DAMAGE);
        }
    }

}

