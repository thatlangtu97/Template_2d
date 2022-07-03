using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AirAttackState", menuName = "CoreGame/State/AirAttackState")]
public class AirAttackState : State
{
    public float timeTriggerGroundPlatform=.3f;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        controller.componentManager.attackAirCount += 1;
        CastSkill();

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            controller.componentManager.Rotate();
                controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove, controller.componentManager.rgbody2D.velocity.y);
                
//                Vector2 velocityAttack = new Vector2( controller.componentManager.speedMove * eventCollectionData[idState].curveX.Evaluate(timeTrigger),
//                    eventCollectionData[idState].curveY.Evaluate(timeTrigger));
//
            if (timeTrigger >= eventCollectionData[idState].durationAnimation)
            {
                controller.ChangeState(NameState.FallingState);
            }
            else
            {
                if (timeTrigger > timeTriggerGroundPlatform)
                {
                    if (controller.componentManager.checkGround())
                    {
                        if (controller.componentManager.speedMove != 0)
                        {
                            controller.ChangeState(NameState.MoveState);
                        }
                        else
                        {
                            controller.ChangeState(NameState.IdleState);
                        }
                    }
                }
            }
        }
        else
        {
            if (controller.componentManager.checkGround() == true)
            {
                if (controller.componentManager.speedMove != 0)
                {
                    controller.ChangeState(NameState.MoveState);
                }
                else
                {
                    controller.ChangeState(NameState.IdleState);
                }
            }
            else
            {
                controller.ChangeState(NameState.FallingState);
            }
        }

    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
        idState = 0;

    }
    public void CastSkill()
    {
        ResetEvent();
        controller.componentManager.Rotate();
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.rgbody2D.velocity.x, eventCollectionData[idState].curveY.Evaluate(0));
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        if (controller.componentManager.CanDash)
            controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        if (controller.componentManager.CanJump)
            controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (controller.componentManager.isAttack)
        {
            if (idState < eventCollectionData.Count - 1)
            {
                idState = (idState + 1);
                CastSkill();
            }
        }
    }
    public override void OnInputSkill(int idSkill)
    {
        base.OnInputSkill(idSkill);
        if (controller.componentManager.checkGround() == true)
        {
            controller.ChangeState(NameState.SkillState);
        }
        else
        {
            controller.ChangeState(NameState.AirSkillState);
        }
    }
}
