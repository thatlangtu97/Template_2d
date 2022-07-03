using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GamePlayCreator : MonoBehaviour
{
    static GamePlayCreator instance;
    public string heroPrefabName;
    private PoolItem heroPrefab;
    public static GamePlayCreator Instance {
        get { return instance; }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        heroPrefab = Resources.Load<PoolItem>("PrefabCharacter/" + heroPrefabName);
    }
    [Button("SPAWN HERO", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void CreateHero()
    {
        
        StateMachineController statemachine = PoolManager.Spawn<StateMachineController>(heroPrefab.gameObject);
        statemachine.GetComponent<ComponentManager>().SetupEntity();
        GameUIController.instance.stateMachine = statemachine;
        GameUIController.instance.MODIFY(); 
    }

    public void NextWave(int waveID)
    {
        
    }
}
