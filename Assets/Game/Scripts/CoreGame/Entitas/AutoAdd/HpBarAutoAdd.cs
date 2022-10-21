using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class HpBarAutoAdd : AutoAddComponent
{
    [ChildGameObjectsOnly]
    public HPBar prefab;
    public override bool AddComponent(GameEntity e)
    {
        e.AddHPBar(prefab);
        return true;
    }
}
