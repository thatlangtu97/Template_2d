using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveStepByStepState", menuName = "CoreGame/State/MoveStepByStepState")]
    public class MoveStepByStepState : State
    {
        public AnimationCurve curveX;
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

