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
            Cast();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger >= duration)
            {
                if (bufferAttack)
                {
                    idState = (idState+1)%eventCollectionData.Count;
                    Cast();
                }
                else
                {
                    idState = 0;
                    controller.ChangeState(NameState.IdleState);
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
            bufferAttack = true;
        }

        void Cast()
        {
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
            bufferAttack = false;
            ResetTimeTrigger();
        }
    }

}

