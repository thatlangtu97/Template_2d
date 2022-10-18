using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "HitState", menuName = "CoreGame/State/HitState")]
    public class HitState : State
    {
        private EventCollection currentState;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            controller.componentManager.PingPong();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger >= duration)
            {
                if(controller.componentManager.IsGround)
                    controller.ChangeState(NameState.IdleState);
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

        void PingPong()
        {
            controller.transform.DOScale(Vector3.one*.8f, 0.1f ).onComplete+= () =>
                {
                    controller.transform.DOScale(Vector3.one, 0.05f);
                };
        }
    }

}

