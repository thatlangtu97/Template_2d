using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "MoveState", menuName = "CoreGame/State/MoveState")]
    public class MoveState : State
    {
        private EventCollection currentState;
        public bool usePointSpeed;
        [ShowIf("usePointSpeed")] 
        public List<float> pointSpeed = new List<float>();
        protected override void OnBeforeSerialize()
        {
            if (eventCollectionData == null) return;
            if (eventCollectionData.Count==0 ) return;

            if (pointSpeed.Count < eventCollectionData.Count)
            {
                pointSpeed.Add(0f);
            }
            else
            {
                if (pointSpeed.Count > eventCollectionData.Count)
                {
                    pointSpeed.RemoveAt(pointSpeed.Count-1);
                }
            }
        }
        public override void EnterState()
        {
            base.EnterState();
            
            if (usePointSpeed)
            {
                idState = -1;
                CheckSpeed();
            }
            else
            {
                idState = 0;
                currentState = eventCollectionData[idState];
                PlayAnim(currentState);
            }
        }

        void CheckSpeed()
        {
            int tempIdState = 0;
            for (int i = 0; i < pointSpeed.Count; i++)
            {
                if (Mathf.Abs(controller.componentManager.vectorMove.x) > pointSpeed[i] )
                {
                    tempIdState = i;
                }
            }

            if (tempIdState != idState)
            {
                idState = tempIdState;
                currentState = eventCollectionData[idState];
                PlayAnim(currentState);
            }
        }
        public override void UpdateState()
        {
            base.UpdateState();
            if(usePointSpeed)
                CheckSpeed();
            controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.vectorMove.x * controller.componentManager.maxSpeedMove,controller.componentManager.rgbody2D.velocity.y);
            if (controller.componentManager.vectorMove == Vector2.zero)
            {
                controller.ChangeState(NameState.IdleState);
            }
            else
            {
                controller.transform.right = new Vector3(controller.componentManager.vectorMove.x,0);
            }
            
            if (!controller.componentManager.IsGround)
            {
                controller.ChangeState(NameState.FallingState);
            }
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
        
        public override void OnInputJump()
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
        public override void OnInputDash()
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
        
        public override void OnInputSkill(int idSkill)
        {
            base.OnInputSkill(idSkill);
            controller.ChangeState(NameState.SkillState,idSkill);
        }
        public override void OnInputCounter()
        {
            base.OnInputCounter();
            controller.ChangeState(NameState.CounterState);
        }
    }
}

