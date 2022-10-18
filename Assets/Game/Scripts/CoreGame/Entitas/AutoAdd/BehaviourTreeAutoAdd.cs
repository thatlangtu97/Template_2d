using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class BehaviourTreeAutoAdd : AutoAddComponent
{
    //public ConvertToBehaviourTree behaviourTreeComponent;
    public BehaviorTree value;
    public override bool AddComponent(GameEntity e)
    {
        e.AddBehaviourTree(value);
        return true;
    }


}
