using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "CoreGame/State/IdleState")]
    public class IdleState : State
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

