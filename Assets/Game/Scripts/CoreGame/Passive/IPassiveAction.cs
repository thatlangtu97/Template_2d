using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPassiveAction
{
    void OnStart(GameEntity e);
    void Execute();
}

public class IncreaseStat : IPassiveAction
{

    public StatType statType;
    public float value;
    private GameEntity entity;
    public void OnStart(GameEntity e)
    {
        entity = e;
    }

    public void Execute()
    {
        switch (statType)
        {
            case StatType.Attack:
                int currentValue = entity.power.value;
                int applyValue = (int)(currentValue * value);
                entity.power.value = currentValue + applyValue;
                break;
        }
    }

    public void OnExecute()
    {
       
        // Add Flying Text
//        if(entity.isPlayerFlag && statIncrease != EStat.NO_KNOCKDOWN)
//        {
//            GameEntity txe = Contexts.sharedInstance.game.CreateEntity();
//            if (increaseValue >= 0)
//            {
//                CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Buff, statIncrease.ToString() + " + " + increaseValue * 100 + "%", entity.centerPoint.centerPoint.position); }, () => { txe.Destroy(); });
//            }
//            else
//            {
//                CleanUpBufferManager.instance.AddReactiveComponent(() => { txe.AddFlyingText(TextType.Buff, statIncrease.ToString() + " - " + increaseValue * 100 + "%", entity.centerPoint.centerPoint.position); }, () => { txe.Destroy(); });
//            }
//        }
    }
}
