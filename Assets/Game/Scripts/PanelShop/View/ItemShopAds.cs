using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopAds : View
{
//    [Inject] public GlobalData globalData { get; set; }

    [Inject] public AddRewardFromItemSignal AddRewardFromItemSignal { get; set; }

    public CurrencyType currencyType;
    public int value;
    public Text valueText;
    
    public AbsRewardLogic rewardLogic;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        valueText.text = $"{value}";
    }
    public void BuyItem()
    {
        
        rewardLogic = RewardUtils.ParseToRewardLogic(currencyType, value);
        AddRewardParameter parameter = new AddRewardParameter(rewardLogic,delegate {  }, true);
        AddRewardFromItemSignal.Dispatch(parameter); 
        
//        switch (currencyType)
//        {
//            case CurrencyType.gold:
//                DataManager.Instance.CurrencyDataManager.UpGold(value, false);
//                break;
//            case CurrencyType.gem:
//                DataManager.Instance.CurrencyDataManager.UpGem(value, false);
//                break;
//            case CurrencyType.stamina:
//                DataManager.Instance.CurrencyDataManager.UpStamina(value, false);
//                break;
//        }
//        globalData.UpdateDataAllCurrencyView();
    }
}
