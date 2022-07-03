using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpawnState", menuName = "CoreGame/State/SpawnState")]
public class SpawnState : State
{
    public override void EnterState()
    {
        base.EnterState();
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
        if (timeTrigger > eventCollectionData[idState].durationAnimation)
        {
            //controller.ChangeState(NameState.IdleState);
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
}
