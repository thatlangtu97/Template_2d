using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Core.GamePlay;
using Sirenix.OdinInspector;

[System.Serializable]
public class SharedComponentManager : SharedVariable<ComponentManager>
{
    public static implicit operator SharedComponentManager(ComponentManager value) { return new SharedComponentManager { Value = value }; }
}
public class SharedStateMachineManager : SharedVariable<StateMachineController>
{
    public static implicit operator SharedStateMachineManager(StateMachineController value) { return new SharedStateMachineManager { Value = value }; }
}

public class SharedActionData : SharedVariable<ActionData>
{
    public static implicit operator SharedActionData(ActionData value) { return new SharedActionData { Value = value }; } 
}
[System.Serializable]
public class ActionData
{
    public int index;
    public bool isDone;
    public List<int> listAction;
    public List<ActionInfo> ActionInfos;
    [Button("ADD", ButtonSizes.Gigantic), GUIColor(0.4f, 0.8f, 1),]
    public void AddAction(string name)
    {
        
    }
}
[System.Serializable]
public class ActionInfo
{
    [FoldoutGroup("$nameAction")]
    public string nameAction;
    public int idAction;
}



