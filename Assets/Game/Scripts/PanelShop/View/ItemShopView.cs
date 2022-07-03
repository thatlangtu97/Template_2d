using System;
using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopView : View
{
    //[Inject] public GlobalData globalData { get; set; }
    [Inject] public AddRewardFromItemSignal AddRewardFromItemSignal { get; set; }


    [Inject] public CheckAndConsumeCurrencySignal CheckAndConsumeCurrencySignal { get; set; }
    public CurrencyType currencyTypeReward, currencyTypeCost;
    public int cost;
    public int value;
    public Text costText, valueText;

    public AbsRewardLogic rewardLogic;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        costText.text = $"{cost}";
        valueText.text = $"{value}";
    }
    public void BuyItem()
    {
//        if (!IsCanBuy()) return;

        
        
        
        Action Success = delegate { 
            rewardLogic = RewardUtils.ParseToRewardLogic(currencyTypeReward, value);
            AddRewardParameter parameter = new AddRewardParameter(rewardLogic,delegate {  }, true);
            AddRewardFromItemSignal.Dispatch(parameter); 
            
        };
        

        CheckAndConsumeCurrencyParameter param = new CheckAndConsumeCurrencyParameter(currencyTypeCost, cost, Success);
        CheckAndConsumeCurrencySignal.Dispatch(param);
        
//        switch (currencyTypeCost)
//        {
//            case CurrencyType.gold:
//                DataManager.Instance.CurrencyDataManager.DownGold(cost, false);
//                break;
//            case CurrencyType.gem:
//                DataManager.Instance.CurrencyDataManager.DownGem(cost, false);
//                break;
//        }

        //globalData.UpdateDataAllCurrencyView();
    }

//    public bool IsCanBuy()
//    {
//        switch (currencyTypeCost)
//        {
//            case CurrencyType.gold:
//                return DataManager.Instance.CurrencyDataManager.gold >= cost;
//                
//            case CurrencyType.gem:
//                return DataManager.Instance.CurrencyDataManager.gem >= cost;
//        }
//        return false;
//    }
}
