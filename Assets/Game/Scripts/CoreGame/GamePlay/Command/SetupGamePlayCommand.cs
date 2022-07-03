using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

public class SetupGamePlayCommand : Command
{
    [Inject] public PopupManager PopupManager { get; set; }
    public override void Execute()
    {
        Debug.Log("Setup");
        EnemySpawnController.Instance.Setup();
        PoolManager.instance.SetupPool();
//        PopupManager.sceneKey = SceneKey.GamePlay.ToString();
    }
}
