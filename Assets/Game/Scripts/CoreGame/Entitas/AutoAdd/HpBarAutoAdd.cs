using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarAutoAdd : AutoAddComponent
{
    public HPBarUI prefab;
    public bool left;
    public override bool AddComponent(GameEntity e)
    {
        HPBarUI temp = GameUIController.instance.SpawnHPBar(prefab, left);
        e.AddHealthBarUI(temp);
        return true;
    }
//    public override void CoppyData()
//    {
//        base.CoppyData();
//    }
}
