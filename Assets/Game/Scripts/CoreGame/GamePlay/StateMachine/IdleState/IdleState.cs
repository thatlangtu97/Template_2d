using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "IdleState", menuName = "CoreGame/State/IdleState")]
    public class IdleState : State
    {
        private EventCollection currentEvent;
        private float duration;
        public override void EnterState()
        {
            base.EnterState();
            CastSkill();
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if (timeTrigger > duration)
            {
                idState = (idState + 1) % eventCollectionData.Count;
                CastSkill();
            }
            
        }
        public override void ExitState()
        {
            base.ExitState();

        }
        void CastSkill()
        {
            currentEvent = eventCollectionData[idState];
            controller.PlayAnim(currentEvent.nameTrigger,currentEvent.typeAnim,currentEvent.timeStart);
            duration = currentEvent.duration;
            ResetEvent();

        }

        public override void OnInputAttack()
        {
            base.OnInputAttack();
            controller.ChangeState(NameState.AttackState,0);
        }
    } 
}

