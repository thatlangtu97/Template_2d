using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "RepostedState", menuName = "CoreGame/State/RepostedState")]
    public class RepostedState : State
    {
        private EventCollection currentEvent;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            currentEvent = eventCollectionData[idState];
            duration = currentEvent.duration;
            controller.PlayAnim(currentEvent.nameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger > duration)
            {
                controller.ChangeState(NameState.IdleState);
            }
        }
        public override void ExitState()
        {
            base.ExitState();
        }
    }

}

