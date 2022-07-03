using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUtils
{
    public static AbsRewardLogic ParseToRewardLogic(CurrencyType type,int value)
    {
        switch (type)
        {
            case CurrencyType.gold:
                return new GoldRewardLogic(value);
            case CurrencyType.gem:
                return new GemRewardLogic(value);
            case CurrencyType.stamina:
                return new StaminaRewardLogic(value);
        }
        return null;
    }

    public static AbsRewardLogic ParseToRewardLogic(EquipmentData data)
    {
        return new EquipmentRewardLogic(data);
    }
}