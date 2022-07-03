using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGamePassiveCondition
{
    GameEntity parentEntity { get; set; }
    void OnStart(GameEntity entity);
    bool PassCondition();
    void Reset();
    string GetName();
}

public class ApplyOnStart : IGamePassiveCondition
{
    public GameEntity parentEntity { get; set; }

    public void OnStart(GameEntity entity)
    {
        parentEntity = entity;
    }
    
    public bool PassCondition()
    {
        return true;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        return "Apply On Start";
    }
}
