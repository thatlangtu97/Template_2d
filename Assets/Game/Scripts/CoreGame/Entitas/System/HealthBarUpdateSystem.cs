using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;
using Entitas;
public class HealthBarUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
//    public List<GameEntity> ListEntity= new List<GameEntity>();
    public HealthBarUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.HealthBarUI));
//        ListEntity = new List<GameEntity>();
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            e.healthBarUI.hpBarUI.Setvalue(e,e.health.health,e.health.maxHealth);
//            if (!ListEntity.Contains(e))
//            {
//                ListEntity.Add(e);
//            }
        }
    }

}
