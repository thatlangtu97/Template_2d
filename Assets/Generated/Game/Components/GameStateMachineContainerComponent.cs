//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public StateMachineContainerComponent stateMachineContainer { get { return (StateMachineContainerComponent)GetComponent(GameComponentsLookup.StateMachineContainer); } }
    public bool hasStateMachineContainer { get { return HasComponent(GameComponentsLookup.StateMachineContainer); } }

    public void AddStateMachineContainer(StateMachineController newValue) {
        var index = GameComponentsLookup.StateMachineContainer;
        var component = (StateMachineContainerComponent)CreateComponent(index, typeof(StateMachineContainerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceStateMachineContainer(StateMachineController newValue) {
        var index = GameComponentsLookup.StateMachineContainer;
        var component = (StateMachineContainerComponent)CreateComponent(index, typeof(StateMachineContainerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveStateMachineContainer() {
        RemoveComponent(GameComponentsLookup.StateMachineContainer);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherStateMachineContainer;

    public static Entitas.IMatcher<GameEntity> StateMachineContainer {
        get {
            if (_matcherStateMachineContainer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StateMachineContainer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStateMachineContainer = matcher;
            }

            return _matcherStateMachineContainer;
        }
    }
}
