﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlySkillState", menuName = "CoreGame/State/FlySkillState")]
public class FlySkillState : State
{
    public bool useCheckEnemyForwark=false;
    public override void EnterState()
    {
        base.EnterState();

        CastSkill();
        controller.componentManager.rgbody2D.velocity = Vector2.zero;

    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeTrigger),
                eventCollectionData[idState].curveY.Evaluate(timeTrigger));
            Vector2 force = new Vector2(velocityAttack.x * controller.transform.localScale.x,
                velocityAttack.y * controller.transform.localScale.y);
            if (!useCheckEnemyForwark)
            {
               
                controller.componentManager.rgbody2D.position += force * Time.deltaTime;
            }
            else
            {
                bool isEnemyForwark = controller.componentManager.checkEnemyForwark();
                if (!isEnemyForwark)
                {
                    controller.componentManager.rgbody2D.position += force * Time.deltaTime;
                }
            }
        }
        else
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
    public override void ExitState()
    {
        base.ExitState();
    }
    public void CastSkill()
    {
        ResetEvent();
        controller.componentManager.Rotate();
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
    }
    public override void OnInputDash()
    {
        if (timeTrigger >= eventCollectionData[idState].durationAnimation && controller.componentManager.CanDash)
        {
            base.OnInputDash();
            controller.ChangeState(NameState.DashState);
        }
    }
    public override void OnInputJump()
    {
        if (timeTrigger >= eventCollectionData[idState].durationAnimation && controller.componentManager.CanJump)
        {
            base.OnInputJump();
            controller.ChangeState(NameState.JumpState);
        }
    }
    public override void OnInputMove()
    {
        if (timeTrigger >= eventCollectionData[idState].durationAnimation && controller.componentManager.checkGround() == true)
        {
            base.OnInputMove();
            controller.ChangeState(NameState.MoveState);
        }
    }
    public override void OnInputSkill(int idSkill)
    {
//        if (timeTrigger >= eventCollectionData[idState].durationAnimation)
//        {
//            base.OnInputSkill(idSkill);
//            idState = idSkill;
//            EnterState();
//        }

    }
    public override void OnInputAttack()
    {
        if (timeTrigger >= eventCollectionData[idState].durationAnimation)
        {
            base.OnInputAttack();
            if (controller.componentManager.checkGround() == true)
            {
                controller.ChangeState(NameState.AttackState);
            }
            else
            {
                if (controller.componentManager.CanAttackAir)
                    controller.ChangeState(NameState.AirAttackState);
            }
        }
    }
}
