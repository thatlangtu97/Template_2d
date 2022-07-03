using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "KnockUpState", menuName = "CoreGame/State/KnockUpState")]
public class KnockUpState : State
{
//    float timeCount = 0;
    public override void EnterState()
    {
        base.EnterState();
//        timeCount = 0;
        controller.SetTrigger(eventCollectionData[idState].NameTrigger,eventCollectionData[idState].typeAnim,eventCollectionData[idState].timeStart);
//        if (entity.hasBehaviourTree)
//        {
//            entity.behaviourTree.value.DisableBehavior();
//        }
        controller.componentManager.DisableBehavior();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (timeTrigger < eventCollectionData[idState].durationAnimation)
        {
            Vector2 velocityAttack = new Vector2(eventCollectionData[idState].curveX.Evaluate(timeTrigger),
                eventCollectionData[idState].curveY.Evaluate(timeTrigger));
            Vector2 velocityFinal = new Vector2(velocityAttack.x * controller.transform.localScale.x,
                velocityAttack.y * controller.transform.localScale.y);
            controller.componentManager.rgbody2D.velocity = velocityFinal;
        }
        else
        {
            if (controller.componentManager.checkGround() == true )
            {
                controller.ChangeState(NameState.IdleState);
            }
        }
        
    }
    public override void ExitState()
    {
        base.ExitState();
//        if (entity.hasBehaviourTree)
//        {
//            entity.behaviourTree.value.EnableBehavior();
//        }
        controller.componentManager.EnableBehavior();
    }
    public override void OnInputDash()
    {
        base.OnInputDash();
        controller.ChangeState(NameState.RollOutState);
    }
}
