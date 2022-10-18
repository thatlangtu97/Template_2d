using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;

public class StateMachineAutoAdd : AutoAddComponent
{
    public StateMachineController value;
    public override bool AddComponent(GameEntity e)
    {

        e.AddStateMachineContainer(value);
        value.InitStateMachine();

        return true;
    }


}
