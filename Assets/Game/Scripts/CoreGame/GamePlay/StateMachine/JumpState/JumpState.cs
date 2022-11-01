using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Core.GamePlay
{
    [CreateAssetMenu(fileName = "JumpState", menuName = "CoreGame/State/JumpState")]
    public class JumpState : State
    {
        private EventCollection currentState;
        public LayerMask layerStop;
        public float distanceCheck;
        public Vector3 LocalPosition;
        public Vector3 SizeBoxCastCheckWall;
        public override void EnterState()
        {
            base.EnterState();
            currentState = eventCollectionData[idState];
            PlayAnim(currentState);
            PingPongJump();
        }
        void Move()
        {
            controller.componentManager.Rotate();
            Vector2 tempVelocity = new Vector2( controller.componentManager.vectorMove.x * controller.componentManager.maxSpeedMove, currentState.curveY.Evaluate(timeTrigger));
            if(Physics2D.BoxCast(controller.transform.position + LocalPosition,SizeBoxCastCheckWall,0,controller.transform.right,distanceCheck,layerStop).collider != null)
            {
                tempVelocity.x =0;
            }
            controller.componentManager.rgbody2D.velocity = tempVelocity;
        }
        public override void UpdateState()
        {
            base.UpdateState();
            Move();

            if (timeTrigger > currentState.durationAnimation)
            {
                if (controller.componentManager.IsGround)
                {
                    controller.ChangeState(NameState.LandingState);
                    return;
                }
                
                controller.ChangeState(NameState.FallingState);
                idState = 0;

            }
        }
        public override void ExitState()
        {
            base.ExitState();
        }

        void PingPongJump()
        {
            controller.transform.DOScale(new Vector3(1f, .7f, 0f), 0.01f).onComplete += () =>
            {
                controller.transform.DOScale(new Vector3(1f, 1f, 0f), 0.1f);
            };
        }
        public override void OnInputDash()
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }

        public override void OnInputAttack()
        {
            base.OnInputAttack();
            controller.ChangeState(NameState.AirAttackState);
        }

//        private bool doubleJump = false;
        public override void OnInputJump()
        {
            base.OnInputJump();
            if (controller.componentManager.isDoubleJump == false)
            {
                base.EnterState();
                if(idState +1 >= eventCollectionData.Count ) return;
                
                currentState = eventCollectionData[idState+1];
                PlayAnim(currentState);
                PingPongJump();
                controller.componentManager.isDoubleJump = true;
            }
        }
    }
}

