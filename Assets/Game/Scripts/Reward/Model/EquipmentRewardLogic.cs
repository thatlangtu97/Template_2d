using System.Collections;
using System.Collections.Generic;
using strange.extensions.injector.api;
using UnityEngine;

public class EquipmentRewardLogic : AbsRewardLogic
{
    public EquipmentData equipmentData;

    public EquipmentRewardLogic(EquipmentData value)
    {
        equipmentData = value;
    }
    public override AbsRewardLogic AddReward(IInjectionBinder injectionBinder)
    {
        injectionBinder.GetInstance<AddEquipmentSignal>().Dispatch(new DataEquipmentRewardParameter(equipmentData));
        return this;
    }

    public override Sprite Icon()
    {
        return EquipmentLogic.GetEquipmentConfigById(equipmentData.idConfig).GearIcon;
    }

    public override Color ColorBorder()
    {
        return EquipmentLogic.GetColorByRarity( equipmentData.rarity);
    }

    public override Sprite BackGround()
    {
        return EquipmentLogic.GetBackGroundByRarity(equipmentData.rarity);
    }

    public override string ValueText()
    {
        return $"Lv.{equipmentData.level}";
    }
}
