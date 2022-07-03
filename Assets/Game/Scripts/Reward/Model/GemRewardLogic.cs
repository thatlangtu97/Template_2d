using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public class GemRewardLogic : AbsRewardLogic
{
    public int value;
    
    public GemRewardLogic(){}
    
    public GemRewardLogic(int value)
    {
        this.value = value;
    }
    
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddGemSignal>().Dispatch(value);
        return this;
    }

    public override Sprite Icon()
    {
        return ScriptableObjectData.ResourceIconCollection.GetResourceIcon(CurrencyType.gem);
    }
    
    public override Color ColorBorder()
    {
        return EquipmentLogic.GetColorByRarity(Rarity.common);
    }

    public override Sprite BackGround()
    {
        return ScriptableObjectData.ResourceIconCollection.GetResourceBackGround(CurrencyType.gem);
    }

    public override string ValueText()
    {
        return value.ToString();
    }
}