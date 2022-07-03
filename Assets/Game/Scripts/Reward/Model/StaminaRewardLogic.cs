using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;


public class StaminaRewardLogic : AbsRewardLogic
{
    public int value;
    
    public StaminaRewardLogic(){}
    
    public StaminaRewardLogic(int value)
    {
        this.value = value;
    }
    
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddStaminaSignal>().Dispatch(value);
        return this;
    }

    public override Sprite Icon()
    {
        return ScriptableObjectData.ResourceIconCollection.GetResourceIcon(CurrencyType.stamina);
    }
    
    public override Color ColorBorder()
    {
        return EquipmentLogic.GetColorByRarity(Rarity.common);
    }

    public override Sprite BackGround()
    {
        return ScriptableObjectData.ResourceIconCollection.GetResourceBackGround(CurrencyType.gold);
    }

    public override string ValueText()
    {
        return value.ToString();
    }
}