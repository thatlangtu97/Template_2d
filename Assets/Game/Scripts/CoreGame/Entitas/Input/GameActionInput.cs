using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionInput : MonoBehaviour
{
    public GameEntity entity;

    public void Awake()
    {
        
    }

    public void OnClick()
    {
        entity.gameInput.action.Invoke();
    }
}
