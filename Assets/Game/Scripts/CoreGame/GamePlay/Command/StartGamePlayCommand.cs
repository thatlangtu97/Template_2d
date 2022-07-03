using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class StartGamePlayCommand : Command
{
    public override void Execute()
    {
        GamePlayCreator.Instance.CreateHero();
        EnemySpawnController.Instance.TestSpawn();
        Debug.Log("Spawn e");
    }
}
