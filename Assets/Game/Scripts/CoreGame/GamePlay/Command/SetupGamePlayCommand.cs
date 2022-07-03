using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SetupGamePlayCommand : Command
{
    public override void Execute()
    {
        Debug.Log("Setup");
        EnemySpawnController.Instance.Setup();
        PoolManager.instance.SetupPool();
    }
}
