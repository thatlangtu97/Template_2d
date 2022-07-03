using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class ProjectileMoveBezierSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    UpdateMoveBezierJobSystem updateMoveBezierJob;
    public ProjectileMoveBezierSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.ProjectileContainer));
        updateMoveBezierJob = new UpdateMoveBezierJobSystem(context.game, 4);
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            e.projectileContainer.value.projectileCollider.UpdateCollider();
            e.projectileContainer.value.projectileMovement.UpdatePosition();
        }
        updateMoveBezierJob.Execute();
    }
    public class UpdateMoveBezierJobSystem : JobSystem<GameEntity>
    {
        public UpdateMoveBezierJobSystem(GameContext context, int threads) :
            base(context.GetGroup(GameMatcher.AllOf(GameMatcher.ProjectileContainer)), threads)
        {
        }

        protected override void Execute(GameEntity entity)
        {
            entity.projectileContainer.value.projectileMovement.CaculatePosition();
        }

    }

}
