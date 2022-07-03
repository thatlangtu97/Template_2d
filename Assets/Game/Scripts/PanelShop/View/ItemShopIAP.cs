using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopIAP : View
{
    //[Inject] public GlobalData globalData { get; set; }
    [Inject] public AddRewardFromItemSignal AddRewardFromItemSignal { get; set; }
    public CurrencyType currencyType;
    public int value;
    public Text valueText;
    public string bundleIAP;
    public AbsRewardLogic rewardLogic;
    protected override void Awake()
    {
        base.Awake();
        base.CopyStart();
        valueText.text = $"{value}";
    }
    public void BuyItem()
    {
        //todo: send Buy bundleIAP
        RewardValue();
    }
    public void RewardValue()
    {
        rewardLogic = RewardUtils.ParseToRewardLogic(currencyType, value);
        AddRewardParameter parameter = new AddRewardParameter(rewardLogic,delegate {  }, true);
        AddRewardFromItemSignal.Dispatch(parameter); 
        
    }
}
