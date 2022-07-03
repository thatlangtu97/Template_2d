using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

public class GamePlayContext : MVCSContext
{
    private readonly GamePlayContextView view;
    
    public GamePlayContext(GamePlayContextView view) : base(view, true)
    {
        this.view = view;
    }

    protected override void mapBindings()
    {
        base.mapBindings();
        commandBinder.Bind<StartGamePlaySignal>()
            .To<SetupGamePlayCommand>()
            .To<StartGamePlayCommand>().InSequence();
    }
    public override void Launch()
    {
        injectionBinder.GetInstance<StartGamePlaySignal>().Dispatch();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }
}
