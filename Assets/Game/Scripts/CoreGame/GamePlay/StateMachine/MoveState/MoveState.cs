using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveState", menuName = "CoreGame/State/MoveState")]
    public class MoveState : State
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
        }
        public override void ExitState()
        {
            base.ExitState();
        }
        public override void OnInputAttack()
        {
            base.OnInputAttack();
            controller.ChangeState(NameState.AttackState);
        }
    }
}

