using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public abstract class AbsRewardLogic 
{
    
    public abstract AbsRewardLogic AddReward(IInjectionBinder injectionBinder);
    public abstract Sprite Icon();
    
    public abstract Color ColorBorder();

    public abstract Sprite BackGround();
    
    public abstract string ValueText();
}
