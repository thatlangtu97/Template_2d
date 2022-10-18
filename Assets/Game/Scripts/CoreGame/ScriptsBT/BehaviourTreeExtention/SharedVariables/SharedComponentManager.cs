using BehaviorDesigner.Runtime;
using Core.GamePlay;
[System.Serializable]
public class SharedComponentManager : SharedVariable<ComponentManager>
{
    public static implicit operator SharedComponentManager(ComponentManager value) { return new SharedComponentManager { Value = value }; }
}
public class SharedStateMachineManager : SharedVariable<StateMachineController>
{
    public static implicit operator SharedStateMachineManager(StateMachineController value) { return new SharedStateMachineManager { Value = value }; }
}


