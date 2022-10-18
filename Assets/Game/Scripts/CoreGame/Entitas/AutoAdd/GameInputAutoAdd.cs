using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputAutoAdd : MonoBehaviour, IAutoAdd<GameEntity>
{
    public GameActionInput gameAction;
    public void AddComponent(ref GameEntity e)
    {
        e.AddGameInput(gameAction.OnClick);
    }
}
