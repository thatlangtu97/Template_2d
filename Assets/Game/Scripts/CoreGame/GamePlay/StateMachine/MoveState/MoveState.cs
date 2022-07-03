using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MoveState", menuName = "CoreGame/State/MoveState")]
public class MoveState : State
{
    bool isFailing = false;
    public float timeTransition = .15f;
    public override void EnterState()
    {
        base.EnterState();
        switch (controller.previousNameState)
        {
            case NameState.FallingState:
            case NameState.JumpState:
            case NameState.AirAttackState:
                controller.SetTrigger("jumptomove",AnimationTypeState.Trigger,0);
                break;
            case NameState.DashState:
            case NameState.DashAttackState:
                controller.SetTrigger("dashtomove",AnimationTypeState.Trigger,0);
                break;

            default: 
                controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
                break;
        }
        
        //controller.animator.SetTrigger(eventCollectionData[idState].NameTrigger);
        isFailing = false;
        controller.componentManager.ResetJumpCount();
        controller.componentManager.ResetDashCount();
        controller.componentManager.ResetAttackAirCount();
    }
    public override void UpdateState()
    {
        if(timeTrigger>timeTransition)
            controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
        base.UpdateState();
        controller.componentManager.rgbody2D.velocity = new Vector2(controller.componentManager.speedMove * eventCollectionData[idState].curveX.Evaluate(timeTrigger), controller.componentManager.rgbody2D.velocity.y);
        controller.componentManager.Rotate();
        if (controller.componentManager.checkGround() == false)
        {
            controller.ChangeState(NameState.FallingState);
        }
        else
        {
            if (controller.componentManager.speedMove == 0)
            {
                controller.ChangeState(NameState.IdleState);
            }
            else
            {
                if (isFailing == true)
                {
                    EnterState();
                }
            }
        }
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
