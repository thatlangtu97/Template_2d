using Entitas;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class StateMachineUpdateSystem : IExecuteSystem
{
    public readonly Contexts context;
    readonly IGroup<GameEntity> entities;
    //UpdateMecanimJobSystem updateMecanimJobSystem;
    public StateMachineUpdateSystem(Contexts _contexts)
    {
        context = _contexts;
        entities = context.game.GetGroup(GameMatcher.AllOf(GameMatcher.StateMachineContainer));
        //updateMecanimJobSystem = new UpdateMecanimJobSystem(context.game, 1);
    }
    public void Execute()
    {
        foreach (var e in entities.GetEntities())
        {
            e.stateMachineContainer.value.UpdateState();
        }
//        NativeArray<float2> position = new NativeArray<float2>(1,Allocator.TempJob);
//        var job = new jobupdate(){position = position};
//        JobHandle jobHandle = job.Schedule(1, 1);
//        jobHandle.Complete();
//        position.Dispose();
    }
    
//    public struct jobupdate : IJobParallelFor
//    {
//        public NativeArray<float2> position;
//        public void Execute(int index)
//        {
//            position[index] += new float2(1f,1f);
//            UnityEngine.Debug.Log(position[index]);
//        }
//    }
}
