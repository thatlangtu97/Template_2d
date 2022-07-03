using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "AttackState", menuName = "CoreGame/State/AttackState")]
public class AttackState : State
{
    public List<float> timeBuffers = new List<float>();
    protected override void OnBeforeSerialize()
    {
        if (eventCollectionData == null) return;
        if (eventCollectionData.Count==0 ) return;

        if (timeBuffers.Count < eventCollectionData.Count)
        {
            timeBuffers.Add(0f);
        }
        else
        {
            if (timeBuffers.Count > eventCollectionData.Count)
            {
                timeBuffers.RemoveAt(timeBuffers.Count-1);
            }
        }
    }
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
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeTrigger), eventCollectionData[idState].curveY.Evaluate(timeTrigger));
            Vector2 velocityFinal = new Vector2(velocityAttack.x * controller.transform.localScale.x,velocityAttack.y * controller.transform.localScale.y);
            controller.componentManager.rgbody2D.velocity = velocityFinal;
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
        controller.componentManager.Rotate();
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
        controller.SetSpeed(eventCollectionData[idState].curveSpeedAnimation.Evaluate(timeTrigger));
        controller.componentManager.rgbody2D.velocity = Vector2.zero;
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
        controller.ChangeState(NameState.SkillState);
    }
}
