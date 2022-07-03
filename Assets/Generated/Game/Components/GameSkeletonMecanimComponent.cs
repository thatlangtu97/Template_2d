//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SkeletonMecanimComponent skeletonMecanim { get { return (SkeletonMecanimComponent)GetComponent(GameComponentsLookup.SkeletonMecanim); } }
    public bool hasSkeletonMecanim { get { return HasComponent(GameComponentsLookup.SkeletonMecanim); } }

    public void AddSkeletonMecanim(Spine.Unity.SkeletonMecanim newMacanim) {
        var index = GameComponentsLookup.SkeletonMecanim;
        var component = (SkeletonMecanimComponent)CreateComponent(index, typeof(SkeletonMecanimComponent));
        component.macanim = newMacanim;
        AddComponent(index, component);
    }

    public void ReplaceSkeletonMecanim(Spine.Unity.SkeletonMecanim newMacanim) {
        var index = GameComponentsLookup.SkeletonMecanim;
        var component = (SkeletonMecanimComponent)CreateComponent(index, typeof(SkeletonMecanimComponent));
        component.macanim = newMacanim;
        ReplaceComponent(index, component);
    }

    public void RemoveSkeletonMecanim() {
        RemoveComponent(GameComponentsLookup.SkeletonMecanim);
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

    static Entitas.IMatcher<GameEntity> _matcherSkeletonMecanim;

    public static Entitas.IMatcher<GameEntity> SkeletonMecanim {
        get {
            if (_matcherSkeletonMecanim == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SkeletonMecanim);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSkeletonMecanim = matcher;
            }

            return _matcherSkeletonMecanim;
        }
    }
}
