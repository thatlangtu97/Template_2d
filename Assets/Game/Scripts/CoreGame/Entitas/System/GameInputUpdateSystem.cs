using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class GameInputUpdateSystem : ReactiveSystem<GameEntity>
{
readonly GameContext _gameContext;
    GameEntity targetEnemy;
    public GameInputUpdateSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameInput);
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasGameInput;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity myEntity in entities)
        {
            
        }
    }
}
