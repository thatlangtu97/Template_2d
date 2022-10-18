using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlagAutoAdd : AutoAddComponent
{
    //public ConvertToPlayerFlag value;
    public bool isPlayer;
    public override bool AddComponent(GameEntity e)
    {
        e.AddPlayerFlag(isPlayer);
        return true;
    }

}
