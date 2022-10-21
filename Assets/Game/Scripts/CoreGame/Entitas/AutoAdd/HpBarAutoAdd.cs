using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarAutoAdd : AutoAddComponent
{
    public HPBar prefab;
    public override bool AddComponent(GameEntity e)
    {
        e.AddHPBar(prefab);
        return true;
    }
}
