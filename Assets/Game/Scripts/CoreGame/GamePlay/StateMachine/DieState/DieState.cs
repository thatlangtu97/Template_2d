using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "DieState", menuName = "CoreGame/State/DieState")]
    public class DieState : State
    {
        public bool canRevive;
        public override void EnterState()
        {
            base.EnterState();
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
