using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using Sirenix.OdinInspector;
using UnityEngine;

public class ToolTestStateMachine : MonoBehaviour
{
    public StateMachineController controller;
    public NameState nameState;

    private void Awake()
    {
        controller = GetComponent<StateMachineController>();
    }

    [Button("CHANGE STATE", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void ChangeState()
    {
        switch (nameState)
        {
            case NameState.AttackState:
                controller.currentState.OnInputAttack();
                break;
                
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("CHANGE STATE"))
        {
            ChangeState();
        }
    }
}
