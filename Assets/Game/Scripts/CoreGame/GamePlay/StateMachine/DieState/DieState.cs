using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "DieState", menuName = "CoreGame/State/DieState")]
    public class DieState : State
    {
        private EventCollection currentState;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            duration = currentState.durationAnimation;
        }
        public override void UpdateState()
        {
            base.UpdateState();
        }
        public override void ExitState()
        {
            base.ExitState();
        }
    }


}
