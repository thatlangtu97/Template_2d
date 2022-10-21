using System;
using System.Collections;
using System.Collections.Generic;
using Core.GamePlay;
using UnityEngine;
using Entitas;
using UniRx;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Random = UnityEngine.Random;


public class TakeDamageSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity targetEnemy;
    StateMachineController stateMachine ;
    DamageInfoSend damageInfoSend;
    List<Vector3> RandomVector;
    CompositeDisposable _disposable;
    int countRandom = 0;
    
    public TakeDamageSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        RandomListPosition();
        _disposable= new CompositeDisposable();
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TakeDamage);
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTakeDamage;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        //NativeArray<GameEntityData> datas = new NativeArray<GameEntityData>(entities.Count,Allocator.TempJob);
        float timedelay = 0;
        int indexAction = 0;
        int maxAction = 5;
        Dictionary<int, List<Action> >actions = new Dictionary<int, List<Action>>();
        foreach (GameEntity entity in entities)
        { 
            targetEnemy = entity.takeDamage.targetEnemy;
            stateMachine = targetEnemy.stateMachineContainer.value;
            damageInfoSend = entity.takeDamage.damageInfoSend;
            Vector3 position = stateMachine.transform.position;
            if (targetEnemy == null)
            {
                return;
            }
            
            if (!stateMachine)
            {
                return;
            }
            this.PostEvent(EventID.TAKE_DAMAGE ,new DataDamageTake(stateMachine,entity.takeDamage.myEntity.stateMachineContainer.value) );
            
            if (!stateMachine.componentManager.HasImmune(Immune.BLOCK))
            {
                int damageTake=(int) (damageInfoSend.damageProperties *
                                      damageInfoSend.damageInfoEvent.damageScale);
                targetEnemy.health.health -= damageTake;

                if (targetEnemy.hasHPBar)
                {
                    targetEnemy.hPBar.value.SetValue(   Mathf.Clamp01((float)targetEnemy.health.health/(float)targetEnemy.health.maxHealth));
                }
                if (targetEnemy.health.health <= 0)
                {
                    stateMachine.ChangeState(NameState.DieState);
                    targetEnemy.health.health = 0;
                    if (targetEnemy.hasPlayerFlag)
                    {
                        if (targetEnemy.playerFlag.isPlayer == true)
                        {
                            
                        }
                    }
                }
                else
                {
                    switch (damageInfoSend.damageInfoEvent.powerCollider) {
                        //case PowerCollider.Node:
                        //    entityEnemy.stateMachineContainer.stateMachine.InvokeAction(e.takeDamage.action);
                        //    break;
                        case PowerCollider.Small:
                        case PowerCollider.Medium:
                        case PowerCollider.Heavy:
                            stateMachine.OnHit(damageInfoSend.action);
                            break;
                        case PowerCollider.KnockDown:
                            stateMachine.OnKnockDown(damageInfoSend.action); 
                            break;
                    }
                
                }

                Vector3 randomPos = RandomVector[countRandom];
                Action temp = delegate
                    {
                        DamageTextManager.AddReactiveComponent(DamageTextType.Normal, damageTake.ToString(),
                            position + randomPos);
                    };
                addAction(actions, temp, maxAction);
            }
            else
            {
                Vector3 randomPos = RandomVector[countRandom];
                Action temp = delegate
                {
                    DamageTextManager.AddReactiveComponent(DamageTextType.Normal,"Block",position + randomPos);
                };
                addAction(actions, temp, maxAction);
            }
            countRandom = (countRandom+1) % (RandomVector.Count);
            //ObjectPool.instance.RecycleEntity(myEntity);
            PoolManager.RecycleEntity(entity);
        }

        invokeAction(actions);

    }

    void addAction(Dictionary<int, List<Action>> dic, Action action, int maxCapacity)
    {
        int key = dic.Keys.Count;
        if (key==0)
        {
            dic.Add(1,new List<Action>{action});
        }
        else
        {
            if (dic[key].Count >= maxCapacity)
            {
                dic.Add(key+1 ,new List<Action>{action});
            }
            else
            {
                dic[key].Add(action);
            }
        }
    }

    void invokeAction(Dictionary<int, List<Action>> dic)
    {
        int timerFrame = 0;
        foreach (var key in dic.Keys)
        {
            Action temp = delegate {  };
            
            Observable.TimerFrame(timerFrame,FrameCountType.Update).Subscribe(l => {
                foreach (var action in dic[key])
                {
                    action.Invoke();
                } 
            }).AddTo(_disposable);
            timerFrame += 1;
        }
        
    }
    public void RandomListPosition()
    {
        RandomVector = new List<Vector3>();
        int count = 0;
        while (count<100)
        {
            count += 1;
            RandomVector.Add(new Vector3(Random.Range(-.5f,.5f),Random.Range(1.5f,2f),0f));
        }
    }
}
