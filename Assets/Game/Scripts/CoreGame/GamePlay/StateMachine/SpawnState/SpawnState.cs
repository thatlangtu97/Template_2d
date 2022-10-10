using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "SpawnState", menuName = "CoreGame/State/SpawnState")]
    public class SpawnState : State
    {
        private EventCollection currentState;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger > currentState.durationAnimation)
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

