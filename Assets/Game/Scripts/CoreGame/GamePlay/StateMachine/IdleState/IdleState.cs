using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "CoreGame/State/IdleState")]
    public class IdleState : State
    {
        private EventCollection currentEvent;
        public override void EnterState()
        {
            base.EnterState();
            currentEvent = eventCollectionData[idState];
            controller.PlayAnim(currentEvent.NameTrigger, currentEvent.typeAnim, currentEvent.timeStart);
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
            controller.ChangeState(NameState.AttackState,0);
        }
    } 
}

