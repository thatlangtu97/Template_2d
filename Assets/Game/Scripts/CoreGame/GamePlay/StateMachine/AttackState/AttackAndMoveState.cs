﻿
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackAndMoveState", menuName = "CoreGame/State/AttackAndMoveState")]
public class AttackAndMoveState : State
{
    bool isEnemyForwark;
    public float timeBuffer = 0.15f;
    public bool useCheckEnemyForwark=true;
    public float scaleSpeed = 0.3f;
    public override void EnterState()
    {
        base.EnterState();
        controller.componentManager.isAttack = true;
        CastSkill();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
//            if(useCheckEnemyForwark)
//                isEnemyForwark = controller.componentManager.checkEnemyForwark();
//            if (!isEnemyForwark)
//            {
//                Vector2 velocityAttack = new Vector2(controller.componentManager.speedMove * scaleSpeed, controller.componentManager.rgbody2D.velocity.y);
//                controller.componentManager.rgbody2D.velocity = velocityAttack;
//            }
            Vector2 velocityAttack = new Vector2(controller.componentManager.speedMove * scaleSpeed, controller.componentManager.rgbody2D.velocity.y);
            controller.componentManager.rgbody2D.velocity = velocityAttack;
            if (controller.componentManager.isBufferAttack == true && (timeTrigger + timeBuffer) > eventCollectionData[idState].durationAnimation)
            {
                timeTrigger += timeBuffer;
            }
            else
            {
                if ((timeTrigger + timeBuffer) > eventCollectionData[idState].durationAnimation)
                {
                    if (controller.componentManager.checkGround() == false)
                    {
                        controller.ChangeState(NameState.FallingState);
                    }
                }
            }
        }
        else
        {
            if (controller.componentManager.isBufferAttack == true)
            {
                
                if (idState+1 >= eventCollectionData.Count)
                {
                    if (controller.componentManager.speedMove != 0)
                    {
                        controller.ChangeState(NameState.MoveState);
                    }
                    else
                    {
                        controller.ChangeState(NameState.IdleState);
                    }
                    return;
                }
                idState = Mathf.Clamp(idState + 1, 0, eventCollectionData.Count - 1);
                CastSkill();
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
    }
    public override void ExitState()
    {
        base.ExitState();
        controller.componentManager.isAttack = false;
        idState = 0;
    }
    public void CastSkill()
    {
        base.ResetTrigger();
        ResetEvent();
        isEnemyForwark = false;
//        if(useCheckEnemyForwark)
//            isEnemyForwark = controller.componentManager.checkEnemyForwark();
        controller.componentManager.Rotate();
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
        //controller.componentManager.rgbody2D.velocity = Vector2.zero;
        controller.componentManager.isBufferAttack = false;

    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.DashState);
    }
    public override void OnInputJump()
    {
        base.OnInputJump();
        controller.ChangeState(NameState.JumpState);
    }
    public override void OnInputMove()
    {
        base.OnInputMove();
        if (idState >= eventCollectionData.Count) return;
        if (timeTrigger >= eventCollectionData[idState].durationAnimation)
            controller.ChangeState(NameState.MoveState);
    }
    public override void OnInputAttack()
    {
        base.OnInputAttack();
        controller.componentManager.isBufferAttack = true;
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
