using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Core.GamePlay;
using UnityEngine;
using Entitas;
using UnityEngine.UI;

#region PlayerFlag
[Game]    
public class PlayerFlagComponent : IComponent 
{
    public bool isPlayer = true;
}
//[System.Serializable]
//public class ConvertToPlayerFlag : ConvertToComponent
//{
//    public bool isPlayer = true;
//    public override IComponent Convert()
//    {
//        return new PlayerFlagComponent(){isPlayer = isPlayer};
//    }
//}
#endregion

#region ProjectileContainer
[Game]
public class ProjectileContainerComponent : IComponent
{
    public ProjectileComponent value;
}
//[System.Serializable]
//public class ConvertToProjectileContainer: ConvertToComponent
//{
//    public ProjectileComponent value;
//    public override IComponent Convert()
//    {
//        return new ProjectileContainerComponent(){value = value};
//    }
//}
#endregion

#region StateMachineContainer
[Game]
public class StateMachineContainerComponent : IComponent
{
    public StateMachineController value;
}
//[System.Serializable]
//public class ConvertToStateMachineContainer :ConvertToComponent
//{
//    public StateMachineController value;
//    public override IComponent Convert()
//    {
//        return new StateMachineContainerComponent() { value = value};
//    }
//}
#endregion

#region DamageText
[Game]
public class DamageTextComponent  : IComponent
{
    public DamageTextType damageTextType;
    public string value;
    public Vector3 position;
}
//[System.Serializable]
//public class ConvertToDamageText : ConvertToComponent
//{
//    public DamageTextType damageTextType;
//    public string value;
//    public Vector3 position;
//    public override IComponent Convert()
//    {
//        return new DamageTextComponent() {damageTextType = damageTextType , value= value, position= position };
//    }
//}

public enum DamageTextType
{
    Normal,
    Block,
}
#endregion

#region Health
[Game]
public class HealthComponent : IComponent
{
    public int health;
    public int maxHealth;
}
//[System.Serializable]
//public class ConvertToHealth : ConvertToComponent
//{
//    public int health;
//    public int maxHealth;
//    public override IComponent Convert()
//    {
//        return new HealthComponent() {health = health, maxHealth = maxHealth};
//    }
//}
#endregion

#region Power
[Game]
public class PowerComponent : IComponent
{
    public int value;
}
//[System.Serializable]
//public class ConvertToPower : ConvertToComponent
//{
//    public int value;
//    public override IComponent Convert()
//    {
//        return new PowerComponent() {value = value};
//    }
//}
#endregion

#region TakeDamage
[Game]
public class TakeDamageComponent : IComponent
{
    public GameEntity myEntity;
    public GameEntity targetEnemy;
    public DamageInfoSend damageInfoSend;
    public TakeDamageComponent(){}
    public TakeDamageComponent(GameEntity myEntity, GameEntity targetEnemy,DamageInfoSend damageInfoSend)
    {
        this.myEntity = myEntity;
        this.targetEnemy = targetEnemy;
        this.damageInfoSend = damageInfoSend;
    }
}
[System.Serializable]
public class ConvertToTakeDamage : ConvertToComponent
{
    public GameEntity myEntity;
    public GameEntity targetEnemy;
    public DamageInfoSend damageInfoSend;
    public override IComponent Convert()
    {
        return new TakeDamageComponent()
            {myEntity = myEntity, targetEnemy = targetEnemy, damageInfoSend = damageInfoSend};
    }
}
#endregion

#region BehaviourTree
[Game]
public class BehaviourTreeComponent : IComponent
{
    public BehaviorTree value;
}
//[System.Serializable]
//public class ConvertToBehaviourTree : ConvertToComponent
//{
//    public BehaviorTree value;
//    public override IComponent Convert()
//    {
//        return new BehaviourTreeComponent() {value = value};
//    }
//}
#endregion


#region SkeletonMecanim
[Game]
public class SkeletonMecanimComponent : IComponent
{
    public Spine.Unity.SkeletonMecanim macanim;
}
#endregion


#region GameInput
[Game]
public class GameInput : IComponent
{
    public Action action;
}
#endregion

public partial class GameContext
{
    public GameEntity playerFlagEntity { get { return GetGroup(GameMatcher.PlayerFlag).GetSingleEntity(); } }
    public GameEntity[] allPlayerFlagEntity { get { return GetGroup(GameMatcher.PlayerFlag).GetEntities(); } }
}

public abstract class ConvertToComponent
{
    public abstract IComponent Convert();
}



