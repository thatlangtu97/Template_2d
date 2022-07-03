using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineSingle : StateMachineController
{
    public NameState nameStateAction;
    public override void InitStateMachine()
    {
        if(dictionaryStateMachine.Count==0)
        SetupState();
        base.InitStateMachine();
//        ChangeState(States[0].NameState);
    }

    public override void OnInputAttack()
    {
        base.OnInputAttack();
        if (currentState != null)
        {
            currentState.OnInputAttack();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (currentNameState == NameState.IdleState)
        {
            ChangeState(nameStateAction);
        }
        
    }
    public override void OnInputSkill(int idSkill)
    {
        if (currentState == null) return;
        base.OnInputSkill(idSkill);
        bool check = false;
        if (dictionaryStateMachine.ContainsKey(NameState.SkillState))
        {
            if (dictionaryStateMachine[NameState.SkillState].eventCollectionData.Count> idSkill)
                dictionaryStateMachine[NameState.SkillState].idState = idSkill;
            else
            {
                check = true;
            }
            
        }
        if (dictionaryStateMachine.ContainsKey(NameState.AirSkillState))
        {
            if (dictionaryStateMachine[NameState.AirSkillState].eventCollectionData.Count > idSkill)
                dictionaryStateMachine[NameState.AirSkillState].idState = idSkill;
            else
            {
                check = true;
            }
        }
        if(!check)
            currentState.OnInputSkill(idSkill);
    }
}
